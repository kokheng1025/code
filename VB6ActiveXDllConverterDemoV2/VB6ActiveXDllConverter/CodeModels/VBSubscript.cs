using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBSubscript : VBBaseAtomicCallStatement
    {
        /*
         * subscript
         * : (valueStmt WS TO WS)? valueStmt
         * 
         * Note for different syntax of array declaration:
         * - Simple 1D: ThisIsArray(2) As String
         * - Simple nD: ThisIsArray(2, 2) As String
         * - n To n (Can start with non-zero index!): ThisIsArray(1 To 3) As String
         * - Using constant as index: ThisIsArray(someConstant) As String
         * - Using arithmetic expression: ThisIsArray(2 + 1) As String
         * - Using logical expression!: ThisIsArray(2 Or 1) As String
         * 
         */

        internal VBSubscript(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
