using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    // Represent NewLine token
    public class VBNewLine : VBBaseModel
    {
        internal IToken NewLineToken { get; }

        internal VBNewLine(IToken newLineToken)
        {
            NewLineToken = newLineToken;
        }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            writer.NewLine();
        }
    }
}
