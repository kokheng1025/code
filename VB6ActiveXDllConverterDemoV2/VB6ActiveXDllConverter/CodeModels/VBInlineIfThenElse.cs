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
    public class VBInlineIfThenElse : VBBaseBlockStatement
    {
        /*
         * ifThenElseStmt
         * : IF WS ifConditionStmt WS THEN WS blockStmt (WS ELSE WS blockStmt)? # inlineIfThenElse
         * | ifBlockStmt ifElseIfBlockStmt* ifElseBlockStmt? END_IF # blockIfThenElse
         * 
         * Note:
         * - Grammar file do not support multiple statements inside If/Else being joined by COLON : (Only one statement is allowed)
         * 
         */

        internal VBInlineIfThenElse(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Block);
        }
    }
}
