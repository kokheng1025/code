using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Parsers.VB6;

namespace VB6ActiveXDllConverter.CodeModels
{
    // For used with any single line statement, like const, dim & etc
    public class VBBaseSingleLineStatement : VBBaseStatement
    {
        internal VBBaseSingleLineStatement(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        { }

        internal override void BeforeVisitChild()
        {
            // Look for comment/new line between last scanned position and this code block
            for (int i = RootModule.LastScanTokenIndex; i < ParserContext.Start.TokenIndex; i++)
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
            RootModule.LastScanTokenIndex = ParserContext.Start.TokenIndex;
        }

        internal override void AfterVisitChild()
        {
            if (RootModule.LastScanTokenIndex > ParserContext.Stop.TokenIndex)
                return; // Already scanned in child node.

            for (int i = RootModule.LastScanTokenIndex; i <= ParserContext.Stop.TokenIndex; i++)
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
            RootModule.LastScanTokenIndex = ParserContext.Stop.TokenIndex + 1;
        }
    }
}
