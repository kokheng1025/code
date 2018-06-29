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
    public class VBSC_Case : VBBaseBlockStatement
    {
        /*
         * sC_Case
         * : CASE WS sC_Cond WS? (COLON? NEWLINE* | NEWLINE +) (block NEWLINE +)?
         * 
         */

        internal VBSC_Case(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Block);
        }
    }
}
