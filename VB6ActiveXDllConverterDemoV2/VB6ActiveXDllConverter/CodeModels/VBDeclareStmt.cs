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
    public class VBDeclareStmt : VBBaseSingleLineStatement
    {
        /*
         * declareStmt
         * : (visibility WS)? DECLARE WS (FUNCTION typeHint? | SUB) WS ambiguousIdentifier typeHint? WS LIB WS STRINGLITERAL (WS ALIAS WS STRINGLITERAL)? (WS? argList)? (WS asTypeClause)?  
         */

        internal VBDeclareStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
