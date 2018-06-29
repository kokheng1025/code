using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    // Represent comment tokens
    public abstract class VBBaseCommentModel : VBBaseModel
    {
        internal IToken CommentToken { get; }

        public bool IsOnNewLine { get; internal set; }

        internal VBBaseCommentModel(IToken commentToken)
        {
            CommentToken = commentToken;
        }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            if (IsOnNewLine)
                writer.WriteIndent();

            writer.Write(CommentToken.Text);
        }
    }
}
