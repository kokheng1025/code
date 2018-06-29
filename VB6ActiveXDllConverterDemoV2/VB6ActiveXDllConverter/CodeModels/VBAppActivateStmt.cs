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
    public class VBAppActivateStmt : VBBaseSingleLineStatement
    {
        /*
         * appActivateStmt
         * : APPACTIVATE WS valueStmt (WS? COMMA WS? valueStmt)?
         * 
         * Note:
         * - valueStmt can be a string literal or even a function call. For conversion purposes should be able to copy as it is.
         * - VB.Net no longer accept the second optional [wait] parameter, just discard it.
         * 
         */

        internal VBAppActivateStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
