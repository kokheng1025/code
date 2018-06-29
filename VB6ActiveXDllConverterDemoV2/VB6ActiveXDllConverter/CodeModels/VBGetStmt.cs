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
    public class VBGetStmt : VBBaseSingleLineStatement
    {
        /*
         * getStmt
         * : GET WS valueStmt WS? COMMA WS? valueStmt? WS? COMMA WS? valueStmt
         * 
         * Ref: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa243376(v=vs.60)
         * Get [#]filenumber, [recnumber], varname
         * Get #1, Position, MyRecord
         */

        internal VBGetStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
