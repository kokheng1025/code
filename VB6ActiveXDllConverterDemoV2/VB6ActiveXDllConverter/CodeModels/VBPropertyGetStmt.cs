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
    public class VBPropertyGetStmt : VBBaseBlockStatement
    {
        /*
         * propertyGetStmt
         * : (visibility WS)? (STATIC WS)? PROPERTY_GET WS ambiguousIdentifier typeHint? (WS? argList)? (WS asTypeClause)? NEWLINE + (block NEWLINE +)? END_PROPERTY
         * 
         */

        internal VBPropertyGetStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Procedure);
        }


        // For Demo
        public VBVisibility PropVisibility;
        public VBText PropStatic;
        public VBAmbiguousIdentifier PropName;
        public VBTypeHint PropTypeHint;
        //private List<VBArg> _propArgs = new List<VBArg>();
        public VBArgList PropArgList;
        public VBAsTypeClause PropAsType;

        internal override void VisitChild()
        {
            // Note: Instead of loop through all child node, need to parse selectively.
            var codeModelVisitor = new VB6CodeModelVisitor(RootModule, this, RootModule.VB6Lexer, RootModule.VB6Parser, RootModule.VB6CommonTokenStream);
            var ctx = ParserContext as VisualBasic6Parser.PropertyGetStmtContext;

            codeModelVisitor.Visit(ctx.visibility());
            PropVisibility = codeModelVisitor.FirstModel as VBVisibility; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.STATIC());
            PropStatic = codeModelVisitor.FirstModel as VBText; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.ambiguousIdentifier());
            PropName = codeModelVisitor.FirstModel as VBAmbiguousIdentifier; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.typeHint());
            PropTypeHint = codeModelVisitor.FirstModel as VBTypeHint; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.argList());
            PropArgList = codeModelVisitor.FirstModel as VBArgList; codeModelVisitor.FirstModel = null;

            codeModelVisitor.Visit(ctx.asTypeClause());
            PropAsType = codeModelVisitor.FirstModel as VBAsTypeClause; codeModelVisitor.FirstModel = null;

            // Parse code block inside prop
            codeModelVisitor.Visit(ctx.block());
        }

        // For Demo
        internal override void AfterVisitChild()
        {
            base.AfterVisitChild();

            var ctx = ParserContext as VisualBasic6Parser.PropertyGetStmtContext;
            this.RootModule.PropsGet.Add(ctx.ambiguousIdentifier().GetText(), this);
        }

        // For Demo
        internal override void Convert(VBNetCodeFileWriter writer)
        {
            var ctx = ParserContext as VisualBasic6Parser.PropertyGetStmtContext;
            var propName = ctx.ambiguousIdentifier().GetText();
            if (!RootModule.PropsGet.ContainsKey(propName))
                return; // Already converted by another prop pair.

            // Check if Let property pair exist
            VBPropertyLetStmt letProp = null;
            if (RootModule.PropsLet.ContainsKey(propName))
                letProp = RootModule.PropsLet[propName];

            // Check if Set property pair exist
            VBPropertySetStmt setProp = null;
            if (RootModule.PropsSet.ContainsKey(propName))
                setProp = RootModule.PropsSet[propName];

            if (letProp != null)
            {
                // Let prop do the conversion
                letProp.Convert(writer);
                return;
            }

            if (setProp != null)
            {
                // Set prop do the conversion
                setProp.Convert(writer);
                return;
            }


            // Reach here if this is a read only prop
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

            if ((letProp == null) && (setProp == null))
            {
                // Read only prop
                writer.Write("ReadOnly ");
            }

            writer.Write("Property ");
            PropName.Convert(writer);


            // Use arg list & return type from Get prop
            if (PropTypeHint != null)
                PropTypeHint.Convert(writer);

            PropArgList.Convert(writer);

            if (PropAsType != null)
            {
                writer.Write(" ");
                PropAsType.Convert(writer);
            }

            // Convert to VB.net Get prop code block (Get ... End Get)
            writer.NewLine();
            writer.Write("Get");
            writer.NewLine();

            var canConvert = false;
            foreach (var model in CodeModels)
            {
                // Actual Get code block starts after a new line
                if (canConvert)
                    model.Convert(writer);
                else
                    canConvert = (model is VBNewLine);
            }
            writer.Write("End Get");

            writer.NewLine();
            writer.Write("End Property");

            // Make sure to remove cache data from module, so that prop conversion is not duplicated.
            if (RootModule.PropsGet.ContainsKey(propName))
                RootModule.PropsGet.Remove(propName);
        }
    }
}
