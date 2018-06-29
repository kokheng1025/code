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
    public class VBTypeStmt_Element : VBBaseSingleLineStatement
    {
        /*
         * typeStmt_Element
         * : ambiguousIdentifier (WS? LPAREN (WS? subscripts)? WS? RPAREN)? (WS asTypeClause)? NEWLINE +
         * 
         * Note:
         * - Similar to variable declaration, except no visibility & type hint.
         * - Must contain either subscripts or asTypeClause. VB6 compile error if both are missing.
         * 
         */

        internal VBTypeStmt_Element(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
