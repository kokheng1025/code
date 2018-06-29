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
    public class VBArgList : VBBaseAtomicCallStatement
    {
        /*
         * argList
         * : LPAREN (WS? arg (WS? COMMA WS? arg)*)? WS? RPAREN
         */

        internal VBArgList(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
