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
    public class VBECS_MemberProcedureCall : VBBaseSingleLineStatement
    {
        /*
         * explicitCallStmt
         * : eCS_ProcedureCall
         * | eCS_MemberProcedureCall
         * 
         * eCS_MemberProcedureCall
         * : CALL WS implicitCallStmt_InStmt? DOT WS? ambiguousIdentifier typeHint? (WS? LPAREN WS? argsCall WS? RPAREN)?
         * 
         */

        internal VBECS_MemberProcedureCall(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
