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
    public class VBLsetStmt : VBBaseSingleLineStatement
    {
        /*
         * lsetStmt
         * : LSET WS implicitCallStmt_InStmt WS? EQ WS? valueStmt
         * 
         * Note:
         * - VB.Net do not support LSet, use PadRight instead.
         * - s1 = s2.PadRight(s1.Length, " "c)    ' this replaces LSet s1 = s2
         */

        internal VBLsetStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
