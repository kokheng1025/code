using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBArgDefaultValue : VBBaseAtomicCallStatement
    {
        /*
         * argDefaultValue
         * : EQ WS? valueStmt
         * 
         */

        internal VBArgDefaultValue(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
