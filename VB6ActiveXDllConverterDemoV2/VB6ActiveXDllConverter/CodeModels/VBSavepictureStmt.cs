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
    /*
     * savepictureStmt
     * : SAVEPICTURE WS valueStmt WS? COMMA WS? valueStmt
     * 
     */

    public class VBSavepictureStmt : VBBaseSingleLineStatement
    {
        internal VBSavepictureStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }

        internal override void BeforeVisitChild()
        {
            base.BeforeVisitChild();

            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa445827(v=vs.60)

            var m = new UnSupportedSavePicture(ParserContext.Start.Line, ParserContext.Start.Column);
            RootModule.AddConversionMessage(m);
            var c = new VBConversionMessage(m);
            ParentCodeBlock.AddCodeModel(c);
        }
    }
}
