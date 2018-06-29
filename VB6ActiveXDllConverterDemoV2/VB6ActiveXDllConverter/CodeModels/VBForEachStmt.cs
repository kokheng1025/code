using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBForEachStmt : VBBaseBlockStatement
    {
        /*
         * forEachStmt
         * : FOR WS EACH WS ambiguousIdentifier typeHint? WS IN WS valueStmt NEWLINE + (block NEWLINE +)? NEXT (WS ambiguousIdentifier)?
         */

        internal VBForEachStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Block);
        }
    }
}
