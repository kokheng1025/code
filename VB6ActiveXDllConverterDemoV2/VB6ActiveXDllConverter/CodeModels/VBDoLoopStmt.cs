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
    public class VBDoLoopStmt : VBBaseBlockStatement
    {
        /*
         * doLoopStmt
         * : DO NEWLINE + (block NEWLINE +)? LOOP
         * | DO WS (WHILE | UNTIL) WS valueStmt NEWLINE + (block NEWLINE +)? LOOP
         * | DO NEWLINE + (block NEWLINE +) LOOP WS (WHILE | UNTIL) WS valueStmt
         * 
         */

        // Assume no loop condition by default
        private bool _isDoConditionExist = false;
        private bool _isLoopConditionExist = false;

        internal VBDoLoopStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Block);
        }
    }
}
