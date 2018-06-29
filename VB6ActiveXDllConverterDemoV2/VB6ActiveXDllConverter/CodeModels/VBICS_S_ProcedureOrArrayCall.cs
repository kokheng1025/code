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
    public class VBICS_S_ProcedureOrArrayCall : VBBaseSingleLineStatement
    {
        /*
         * iCS_S_ProcedureOrArrayCall
         * : (ambiguousIdentifier | baseType | iCS_S_NestedProcedureCall) typeHint? WS? (LPAREN WS? (argsCall WS?)? RPAREN)+ dictionaryCallStmt?
         * 
         */

        internal VBICS_S_ProcedureOrArrayCall(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }


        // For Demo
        internal override void Convert(VBNetCodeFileWriter writer)
        {
            var ctx = ParserContext as VisualBasic6Parser.ICS_S_ProcedureOrArrayCallContext;

            foreach (var model in CodeModels)
            {
                if (model is VBAmbiguousIdentifier)
                {
                    var m = model as VBAmbiguousIdentifier;
                    if (m.ParserContext.GetText().Equals("IsEmpty", StringComparison.InvariantCultureIgnoreCase))
                        writer.Write("IsNothing");
                    else
                        model.Convert(writer);
                }
                else
                {
                    model.Convert(writer);
                }
            }
        }
    }
}
