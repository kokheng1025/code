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
    public class VBMacroConstStmt : VBBaseSingleLineStatement
    {
        /*
         * macroConstStmt
         * : MACRO_CONST WS ambiguousIdentifier WS? EQ WS? valueStmt
         * 
         * Note:
         * - This is a custom rule added to grammar file, since the original open source file do not support this.
         * - Refer: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa262669(v=vs.60)
         * - Syntax is similar to Const, but one constant declaration per line.
         * - Expression can only be litral, other conditional compiler constant, or any combination that includes any or all arithmetic or logical operators except Is.
         * - Private by default
         * 
         */

        internal VBMacroConstStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        { }
    }
}
