using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBRedimSubStmt : VBBaseSingleLineSubStatement
    {
        /*
         * redimSubStmt
         * : implicitCallStmt_InStmt WS? LPAREN WS? subscripts WS? RPAREN (WS asTypeClause)?
         */

        internal VBRedimSubStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
