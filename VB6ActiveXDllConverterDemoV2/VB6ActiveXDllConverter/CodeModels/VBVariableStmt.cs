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
    public class VBVariableStmt : VBBaseSingleLineStatement
    {
        /*
         * variableStmt
         * : (DIM | STATIC | visibility) WS (WITHEVENTS WS)? variableListStmt
         * 
         * variableListStmt
         * : variableSubStmt (WS? COMMA WS? variableSubStmt)*
         * 
         */

        internal VBVariableStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
