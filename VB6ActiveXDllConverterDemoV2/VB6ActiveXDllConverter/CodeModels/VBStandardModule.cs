using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBStandardModule : VBBaseModule
    {
        public bool IsPrivateModule { get; internal set; }

        internal VBStandardModule(VisualBasic6Lexer vb6Lexer, VisualBasic6Parser vb6Parser, CommonTokenStream commonTokenStream, ParserRuleContext context) : base(vb6Lexer, vb6Parser, commonTokenStream, context) { }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            writer.Write($"Module {ModuleName}");
            writer.Indent();
            foreach (var model in CodeModels)
            {
                model.Convert(writer);
            }
            writer.Outdent();
            //writer.NewLine();
            writer.WriteLine("End Module");
        }
    }
}
