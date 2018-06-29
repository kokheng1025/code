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
    public class VBVsImp : VBBaseValueStatement
    {
        /*
         * valueStmt
         * | valueStmt WS? IMP WS? valueStmt                                 # vsImp
         */

        internal VBVsImp(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
