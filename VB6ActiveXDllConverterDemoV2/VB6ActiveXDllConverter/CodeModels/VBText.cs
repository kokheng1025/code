using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    // Represent a fixed text
    public class VBText : VBBaseModel
    {
        internal ITerminalNode Token { get; }
        internal VB6CodeModelFactoryContext FactoryContext { get; }
        internal VBBaseModule RootModule { get; }
        internal VBBaseCodeModel ParentCodeBlock { get; }

        internal VBText(VB6CodeModelFactoryContext factoryContext, ITerminalNode token)
        {
            Token = token;
            FactoryContext = factoryContext;
            RootModule = factoryContext.RootModule;
            ParentCodeBlock = factoryContext.ParentCodeBlock;
        }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            writer.Write(Token.GetText());
        }

        internal void BeforeVisitChild()
        {
            // Look for comment/new line between last scanned position and this code block
            for (int i = RootModule.LastScanTokenIndex; i < Token.Symbol.TokenIndex; i++)
            {
                var token = RootModule.VB6CommonTokenStream.Get(i);
                switch (token.Type)
                {
                    case VisualBasic6Parser.NEWLINE:
                        ParentCodeBlock.AddCodeModel(new VBNewLine(token));
                        break;
                    case VisualBasic6Parser.COMMENT:
                        var isOnNewLine = true;
                        if (token.TokenIndex > 0)
                            isOnNewLine = (RootModule.VB6CommonTokenStream.Get(token.TokenIndex - 1).Type == VisualBasic6Parser.NEWLINE);
                        ParentCodeBlock.AddCodeModel(new VBComment(token, isOnNewLine));
                        break;
                    default:
                        // ignore the rest
                        break;
                }
            }
            // Make sure to set last scan position before visiting all child nodes. This is a shared variable.
            RootModule.LastScanTokenIndex = Token.Symbol.TokenIndex;
        }

        internal void AfterVisitChild()
        {
            if (RootModule.LastScanTokenIndex > Token.Symbol.TokenIndex)
                return; // Already scanned in child node.

            for (int i = RootModule.LastScanTokenIndex; i <= Token.Symbol.TokenIndex; i++)
            {
                var token = RootModule.VB6CommonTokenStream.Get(i);
                switch (token.Type)
                {
                    case VisualBasic6Parser.NEWLINE:
                        ParentCodeBlock.AddCodeModel(new VBNewLine(token));
                        break;
                    case VisualBasic6Parser.COMMENT:
                        var isOnNewLine = true;
                        if (token.TokenIndex > 0)
                            isOnNewLine = (RootModule.VB6CommonTokenStream.Get(token.TokenIndex - 1).Type == VisualBasic6Parser.NEWLINE);
                        ParentCodeBlock.AddCodeModel(new VBComment(token, isOnNewLine));
                        break;
                    default:
                        // ignore the rest
                        break;
                }
            }
            // Make sure to set last scan position to the next token, since the current one has been scanned.
            RootModule.LastScanTokenIndex = Token.Symbol.TokenIndex + 1;
        }
    }
}
