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
    public class VBICS_B_ProcedureCall : VBBaseSingleLineStatement
    {
        /*
         * implicitCallStmt_InBlock
         * : iCS_B_ProcedureCall
         * | iCS_B_MemberProcedureCall
         * 
         * iCS_B_ProcedureCall
         * : certainIdentifier (WS argsCall)?
         * 
         */

        internal VBICS_B_ProcedureCall(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
