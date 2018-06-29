using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBAmbiguousIdentifier : VBBaseAtomicRuleStatement
    {
        /*
         * ambiguousIdentifier
         * : (IDENTIFIER | ambiguousKeyword) +
         * | L_SQUARE_BRACKET (IDENTIFIER | ambiguousKeyword) + R_SQUARE_BRACKET
         * 
         */

        internal VBAmbiguousIdentifier(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
