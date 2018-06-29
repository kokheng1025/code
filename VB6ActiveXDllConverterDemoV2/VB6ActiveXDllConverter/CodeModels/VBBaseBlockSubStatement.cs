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
    // Part of block statement. Like Type & Type element (User defined type)
    public class VBBaseBlockSubStatement : VBBaseStatement
    {
        public VBBaseCommentModel Comment { get; internal set; }

        internal VBBaseBlockSubStatement(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
