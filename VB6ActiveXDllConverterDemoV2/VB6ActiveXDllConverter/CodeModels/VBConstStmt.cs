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
    public class VBConstStmt : VBBaseSingleLineStatement
    {
        // Syntax reference:
        // https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa243294(v=vs.60)
        // If visibility is missing, it is default to Private (https://msdn.microsoft.com/en-us/library/ms235296%28v=VS.100%29.aspx)

        /*
         * constStmt
         * : (publicPrivateGlobalVisibility WS)? CONST WS constSubStmt (WS? COMMA WS? constSubStmt)*
         * 
         * Note:
         * - Can declare one or more Const in one line.
         * - Const expression can only be: Literal, other constant, or any combination that includes all arithmetic or logical operators except Is.
         */

        internal VBConstStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
