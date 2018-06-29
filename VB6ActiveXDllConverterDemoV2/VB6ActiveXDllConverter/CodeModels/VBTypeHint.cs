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
    public class VBTypeHint : VBBaseAtomicRuleStatement
    {
        /*
       * typeHint
       * : AMPERSAND
       * | AT
       * | DOLLAR
       * | EXCLAMATIONMARK
       * | HASH
       * | PERCENT
       * 
       * 
       * Type hint:
       * & - Long
       * @ - Currency
       * $ - String
       * ! - Single
       * # - Double
       * % - Integer
       * 
       */

        internal VBTypeHint(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
