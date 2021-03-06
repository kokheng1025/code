﻿using System;
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
    public class VBTypeStmt : VBBaseBlockStatement
    {
        /*
         * typeStmt
         * : (visibility WS)? TYPE WS ambiguousIdentifier NEWLINE + (typeStmt_Element)* END_TYPE
         * 
         */

        internal VBTypeStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
