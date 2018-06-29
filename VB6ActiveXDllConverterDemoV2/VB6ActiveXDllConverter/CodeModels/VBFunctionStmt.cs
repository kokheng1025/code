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
    public class VBFunctionStmt : VBBaseBlockStatement
    {
        /*
         * functionStmt
         * : (visibility WS)? (STATIC WS)? FUNCTION WS ambiguousIdentifier (WS? argList)? (WS asTypeClause)? NEWLINE + (block NEWLINE +)? END_FUNCTION
         * 
         * Note:
         * - Refer: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa243374(v=vs.60)
         * - MSDN didn't specify Function can use type hint: "Public Function TestFunc$()", but VB6 still accept this syntax...
         * - Follow MSDN format now. Will hit parser error if type hint is used.
         * 
         */

        internal VBFunctionStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Procedure);
        }
    }
}
