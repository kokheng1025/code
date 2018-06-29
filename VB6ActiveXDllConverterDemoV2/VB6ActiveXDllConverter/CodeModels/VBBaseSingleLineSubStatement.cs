using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    // Part of single line statement. Like constStmt & constSubStmt
    public class VBBaseSingleLineSubStatement : VBBaseStatement
    {
        internal VBBaseSingleLineSubStatement(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
