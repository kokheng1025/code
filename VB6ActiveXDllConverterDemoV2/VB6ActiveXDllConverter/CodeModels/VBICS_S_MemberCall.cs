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
    public class VBICS_S_MemberCall : VBBaseSingleLineSubStatement
    {
        /*
         * iCS_S_MemberCall
         * : WS? DOT (iCS_S_VariableOrProcedureCall | iCS_S_ProcedureOrArrayCall)
         * 
         */

        internal VBICS_S_MemberCall(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
