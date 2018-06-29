using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Parsers.VB6;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBConstSubStmt : VBBaseSingleLineSubStatement
    {
        /*
         * constSubStmt
         * : ambiguousIdentifier typeHint? (WS asTypeClause)? WS? EQ WS? valueStmt
         * 
         */

        internal VBConstSubStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
