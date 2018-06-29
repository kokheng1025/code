using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBComment : VBBaseCommentModel
    {
        internal VBComment(IToken commentToken, bool isOnNewLine) : base(commentToken)
        {
            IsOnNewLine = isOnNewLine;
        }
    }
}
