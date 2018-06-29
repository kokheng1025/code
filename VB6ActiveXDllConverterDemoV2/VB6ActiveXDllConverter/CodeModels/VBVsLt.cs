﻿using System;
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
    public class VBVsLt : VBBaseValueStatement
    {
        /*
         * valueStmt
         * | valueStmt WS? LT WS? valueStmt                                  # vsLt
         */

        internal VBVsLt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
