using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBOutputList_Expression : VBBaseSingleLineSubStatement
    {
        internal VBOutputList_Expression(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
