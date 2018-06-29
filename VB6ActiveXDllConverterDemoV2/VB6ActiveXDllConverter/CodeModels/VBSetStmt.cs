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
    public class VBSetStmt : VBBaseSingleLineStatement
    {
        /*
         * setStmt
         * : SET WS implicitCallStmt_InStmt WS? EQ WS? valueStmt
         * 
         */

        internal VBSetStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }


        // For demo
        internal override void VisitChild()
        {
            var ctx = ParserContext as VisualBasic6Parser.SetStmtContext;
            var codeModelVisitor = new VB6CodeModelVisitor(RootModule, this, RootModule.VB6Lexer, RootModule.VB6Parser, RootModule.VB6CommonTokenStream);

            codeModelVisitor.Visit(ctx.implicitCallStmt_InStmt());
            codeModelVisitor.Visit(ctx.valueStmt());
        }

        // For demo
        internal override void Convert(VBNetCodeFileWriter writer)
        {
            CodeModels[0].Convert(writer);
            writer.Write(" = ");
            CodeModels[1].Convert(writer);
        }
    }
}

