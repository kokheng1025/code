using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;
using VB6ActiveXDllConverter.Visitors;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBMacroIfBlockStmt : VBBaseBlockStatement
    {
        /*
         * macroIfBlockStmt
         * : MACRO_IF WS ifConditionStmt WS THEN NEWLINE + (moduleBody NEWLINE +)?
         * 
         * ifConditionStmt
         * : valueStmt
         * 
         * Note:
         * - Refer: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa262671(v=vs.60)
         * - If condition is much simpler. Can only use other conditional compiler constants, literal and operators. Therefore, can be copied as it is for now.
         */
    
        internal VBMacroIfBlockStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.MacroBlock);
        }
    }
}
