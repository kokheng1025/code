using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBBaseAtomicCallStatement : VBBaseStatement
    {
        internal VBBaseAtomicCallStatement(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
