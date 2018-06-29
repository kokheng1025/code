using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBPropertyLetStmt : VBBaseBlockStatement
    {
        /*
         * propertyLetStmt
         * : (visibility WS)? (STATIC WS)? PROPERTY_LET WS ambiguousIdentifier (WS? argList)? NEWLINE + (block NEWLINE +)? END_PROPERTY
         * 
         */

        internal VBPropertyLetStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Procedure);
        }


        // For Demo
        public VBVisibility PropVisibility;
        public VBText PropStatic;
        public VBAmbiguousIdentifier PropName;
        public List<VBArg> PropArgs = new List<VBArg>();
        public VBArg PropLetArg;

        internal override void VisitChild()
        {
            // Note: Instead of loop through all child node, need to parse selectively.
            var codeModelVisitor = new VB6CodeModelVisitor(RootModule, this, RootModule.VB6Lexer, RootModule.VB6Parser, RootModule.VB6CommonTokenStream);
            var ctx = ParserContext as VisualBasic6Parser.PropertyLetStmtContext;

            codeModelVisitor.Visit(ctx.visibility());
            PropVisibility = codeModelVisitor.FirstModel as VBVisibility; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.STATIC());
            PropStatic = codeModelVisitor.FirstModel as VBText; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.ambiguousIdentifier());
            PropName = codeModelVisitor.FirstModel as VBAmbiguousIdentifier; codeModelVisitor.FirstModel = null;

            // Parse the individual param instead of the entire list. Need to know which arg is for property param and which one for Let param.
            int i = 0;
            var allParams = ctx.argList().arg();
            for (i = 0; i < allParams.Length - 1; i++)
            {
                // All args here is property params shared by Get/Let
                codeModelVisitor.Visit(allParams[i]);
                PropArgs.Add(codeModelVisitor.FirstModel as VBArg);
                codeModelVisitor.FirstModel = null;
            }

            // The last one is Let arg (RHS)
            codeModelVisitor.Visit(allParams[i]);
            PropLetArg = codeModelVisitor.FirstModel as VBArg;
            codeModelVisitor.FirstModel = null;

            // Parse code block inside prop
            codeModelVisitor.Visit(ctx.block());
        }

        // For Demo
        internal override void AfterVisitChild()
        {
            base.AfterVisitChild();

            var ctx = ParserContext as VisualBasic6Parser.PropertyLetStmtContext;
            this.RootModule.PropsLet.Add(ctx.ambiguousIdentifier().GetText(), this);
        }

        // For Demo
        internal override void Convert(VBNetCodeFileWriter writer)
        {
            var ctx = ParserContext as VisualBasic6Parser.PropertyLetStmtContext;
            var propName = ctx.ambiguousIdentifier().GetText();
            if (!RootModule.PropsLet.ContainsKey(propName))
                return; // Already converted by another prop pair.

            // Check if Get property pair exist
            VBPropertyGetStmt getProp = null;
            if (RootModule.PropsGet.ContainsKey(propName))
                getProp = RootModule.PropsGet[propName];


            // Convert to VB.net prop format: Public Property PropName(ByVal arg1 As DataType) As DataType
            if (PropVisibility != null)
            {
                PropVisibility.Convert(writer);
                writer.Write(" ");
            }

            if (PropStatic != null)
            {
                PropStatic.Convert(writer);
                writer.Write(" ");
            }

            if (getProp == null)
            {
                // Write only prop
                writer.Write("WriteOnly ");
            }

            writer.Write("Property ");
            PropName.Convert(writer);

            if (getProp != null)
            {
                // Use arg list & return type from Get prop if exist. It is the same with Set prop.
                if (getProp.PropTypeHint != null)
                    getProp.PropTypeHint.Convert(writer);

                getProp.PropArgList.Convert(writer);

                if (getProp.PropAsType != null)
                {
                    writer.Write(" ");
                    getProp.PropAsType.Convert(writer);
                }

            }
            else
            {
                // This is write only prop. Use arg list from Let prop.
                var propLetTypeHint = PropLetArg.CodeModels.OfType<VBTypeHint>().FirstOrDefault();
                if (propLetTypeHint != null)
                    propLetTypeHint.Convert(writer);

                writer.Write("(");
                if (PropArgs.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i < PropArgs.Count - 1; i++)
                    {
                        PropArgs[i].Convert(writer);
                        writer.Write(", ");
                    }
                    PropArgs[i].Convert(writer); // Last one
                }
                writer.Write(")");

                var propLetAsType = PropLetArg.CodeModels.OfType<VBAsTypeClause>().FirstOrDefault();
                if (propLetAsType != null)
                {
                    writer.Write(" ");
                    propLetAsType.Convert(writer);
                }
            }

            var canConvert = false;
            if (getProp != null)
            {
                // Convert to VB.net Get prop code block (Get ... End Get)
                writer.NewLine();
                writer.Write("Get");
                writer.NewLine();

                foreach (var model in getProp.CodeModels)
                {
                    // Actual Get code block starts after a new line
                    if (canConvert)
                        model.Convert(writer);
                    else
                        canConvert = (model is VBNewLine);
                }

                writer.Write("End Get");
            }

            // Convert to VB.net Set prop code block (Set ... End Set)
            writer.NewLine();
            writer.Write("Set(");
            PropLetArg.Convert(writer);
            writer.Write(")");
            writer.NewLine();

            canConvert = false;
            foreach (var model in this.CodeModels)
            {
                // Actual Set code block starts after a new line
                if (canConvert)
                    model.Convert(writer);
                else
                    canConvert = (model is VBNewLine);
            }

            writer.Write("End Set");

            writer.NewLine();
            writer.Write("End Property");

            // Make sure to remove cache data from module, so that prop conversion is not duplicated.
            if (RootModule.PropsGet.ContainsKey(propName))
                RootModule.PropsGet.Remove(propName);

            if (RootModule.PropsLet.ContainsKey(propName))
                RootModule.PropsLet.Remove(propName);
        }
    }
}
