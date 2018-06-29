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
    public class VBRsetStmt : VBBaseSingleLineStatement
    {
        /*
         * rsetStmt
         * : RSET WS implicitCallStmt_InStmt WS? EQ WS? valueStmt
         * 
         * Note:
         * - VB.Net do not support RSet, use PadLeft instead.
         * - s1 = s2.PadLeft(s1.Length, " "c)    ' this replaces RSet s1 = s2
         */

        internal VBRsetStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
