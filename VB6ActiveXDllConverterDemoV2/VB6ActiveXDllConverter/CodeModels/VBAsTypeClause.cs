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
    public class VBAsTypeClause : VBBaseAtomicRuleStatement
    {
        /*
         * asTypeClause
         * : AS WS (NEW WS)? type (WS fieldLength)?
         * 
         * fieldLength
         * : MULT WS? (INTEGERLITERAL | ambiguousIdentifier)
         * 
         */

        internal VBAsTypeClause(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
