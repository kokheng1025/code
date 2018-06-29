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
    public class VBEnumerationStmt_Constant : VBBaseSingleLineStatement
    {
        /*
         * enumerationStmt_Constant
         * : ambiguousIdentifier (WS? EQ WS? valueStmt)? NEWLINE +
         * 
         * Note:
         * - Enum member value is optional, can be 32-bit number (VB6 Long) or constant: valueStmt can be a long literal or identifier
         * 
         */

        internal VBEnumerationStmt_Constant(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
