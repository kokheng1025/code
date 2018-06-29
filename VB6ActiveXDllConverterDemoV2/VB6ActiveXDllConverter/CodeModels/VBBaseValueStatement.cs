using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Parsers.VB6;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBBaseValueStatement : VBBaseStatement
    {
        internal VBBaseValueStatement(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
