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
    public class VBArg : VBBaseAtomicCallStatement
    {
        /*
         * arg
         * : (OPTIONAL WS)? ((BYVAL | BYREF) WS)? (PARAMARRAY WS)? ambiguousIdentifier typeHint? (WS? LPAREN WS? RPAREN)? (WS asTypeClause)? (WS? argDefaultValue)?
         * 
         * argDefaultValue
         * : EQ WS? valueStmt
         * 
         * Refer MSDN: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa243374(v=vs.60)
         * Default value: Any constant or constant expression. Valid for Optional parameters only. If the type is an Object, an explicit default value can only be Nothing.
         * Similar to const, can be fixed value, arithmetic, logical expression or mixed of anyone.
         * 
         */

        internal VBArg(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            var ctx = ParserContext as VisualBasic6Parser.ArgContext;

            // If ByVal/ByRef keyword is missing, VB6 defaults to ByRef, VB.net defaults to ByVal. Must specify ByRef in VB.net during conversion.
            if ((ctx.BYVAL() == null) && (ctx.BYREF() == null))
            {
                foreach (var model in CodeModels)
                {
                    if (model is VBAmbiguousIdentifier)
                    {
                        writer.Write("ByRef ");
                        model.Convert(writer);
                    }
                    else
                    {
                        model.Convert(writer);
                    }
                }
            }
            else
            {
                base.Convert(writer);
            }
        }
    }
}
