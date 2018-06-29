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
    public class VBICS_S_NestedProcedureCall : VBBaseSingleLineSubStatement
    {
        /*
         * iCS_S_NestedProcedureCall
         * : ambiguousIdentifier typeHint? WS? LPAREN WS? (argsCall WS?)? RPAREN
         * 
         */

        internal VBICS_S_NestedProcedureCall(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
