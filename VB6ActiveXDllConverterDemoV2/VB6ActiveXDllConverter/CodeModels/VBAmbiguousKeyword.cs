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
    public class VBAmbiguousKeyword : VBBaseAtomicRuleStatement
    {
        /*
         * Note: 
         * - Grammar rule is quite long, contains a list of VB6 keywords. 
         * - Refer to the grammar file and search for "ambiguousKeyword" rule.
         * 
         */

        internal VBAmbiguousKeyword(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
