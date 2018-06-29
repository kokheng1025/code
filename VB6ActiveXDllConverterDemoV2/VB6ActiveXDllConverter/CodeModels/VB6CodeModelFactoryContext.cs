using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ProjectModels;
using VB6ActiveXDllConverter.CodeModels;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VB6CodeModelFactoryContext
    {
        public VBBaseModule RootModule { get; }
        public VBBaseCodeModel ParentCodeBlock { get; }
        public ParserRuleContext ParserContext { get; }
        public VBScope Scope { get; }

        public VB6CodeModelFactoryContext(VBBaseModule rootModule, VBBaseCodeModel parentCodeBlock, ParserRuleContext context, VBScope scope)
        {
            RootModule = rootModule;
            ParentCodeBlock = parentCodeBlock;
            ParserContext = context;
            Scope = scope;
        }
    }
}
