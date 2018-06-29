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
    public class VBImplementsStmt : VBBaseSingleLineStatement
    {
        /*
         * implementsStmt
         * //   : IMPLEMENTS WS ambiguousIdentifier
         * : IMPLEMENTS WS ambiguousIdentifier (DOT ambiguousIdentifier)*
         * 
         * Note:
         * - (DOT ambiguousIdentifier)* is added manually, original GitHub source do not have it.
         * - Either change Globe source code to remove the . in Implements statement, or add it in the grammar rule file.
         */

        internal VBImplementsStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
