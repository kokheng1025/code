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
    public class VBEnumerationStmt : VBBaseBlockStatement
    {
        /*
         * enumerationStmt
         * : (publicPrivateVisibility WS)? ENUM WS ambiguousIdentifier NEWLINE + (enumerationStmt_Constant)* END_ENUM
         * 
         * enumerationStmt_Constant
         * : ambiguousIdentifier (WS? EQ WS? valueStmt)? NEWLINE +
         * 
         * Note:
         * - If visibility is missing, it is default to Public (https://msdn.microsoft.com/en-us/library/ms235296%28v=VS.100%29.aspx)
         */

        internal VBEnumerationStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
