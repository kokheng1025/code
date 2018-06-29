using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBBaseStatement : VBBaseCodeModel
    {
        public VBBaseModule RootModule { get; }
        public VBBaseCodeModel ParentCodeBlock { get; }


        internal VBBaseStatement(VB6CodeModelFactoryContext factoryContext) : base(factoryContext.ParserContext)
        {
            RootModule = factoryContext.RootModule;
            ParentCodeBlock = factoryContext.ParentCodeBlock;
            Scope = factoryContext.Scope;
        }

        internal override void VisitChild()
        {
            var codeModelVisitor = new VB6CodeModelVisitor(RootModule, this, RootModule.VB6Lexer, RootModule.VB6Parser, RootModule.VB6CommonTokenStream);
            foreach (var item in ParserContext.children)
            {
                codeModelVisitor.Visit(item);
            }
            //codeModelVisitor.PostProcess();
        }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            //// Default implementation is copy as it is. Override this in child classes for conversion.
            //writer.WriteIndent();
            if (CodeModels.Count == 0)
            {
                writer.Write(ParserContext.GetText());
            }
            else
            {
                foreach (var item in CodeModels)
                    item.Convert(writer);
            }
        }
    }
}
