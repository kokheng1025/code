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
    public class VBCaseCondExprIs : VBBaseBlockSubStatement
    {
        /*
         * sC_CondExpr
         * : IS WS? comparisonOperator WS? valueStmt # caseCondExprIs
         * | valueStmt # caseCondExprValue
         * | valueStmt WS TO WS valueStmt # caseCondExprTo
         * 
         */
        internal VBCaseCondExprIs(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
