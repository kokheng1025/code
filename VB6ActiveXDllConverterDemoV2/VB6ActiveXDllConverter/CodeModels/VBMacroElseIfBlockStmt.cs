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
    public class VBMacroElseIfBlockStmt : VBBaseBlockStatement
    {
        /*
         * macroElseIfBlockStmt
         * : MACRO_ELSEIF WS ifConditionStmt WS THEN NEWLINE + (moduleBody NEWLINE +)?
         * 
         * ifConditionStmt
         * : valueStmt
         * 
         */

        public string ConditionExpression { get; }

        internal VBMacroElseIfBlockStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.MacroBlock);
        }
    }
}
