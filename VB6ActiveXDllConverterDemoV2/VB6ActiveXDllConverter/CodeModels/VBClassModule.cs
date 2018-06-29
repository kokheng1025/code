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
    public class VBClassModule : VBBaseModule
    {
        /*
         * Reference: https://msdn.microsoft.com/en-us/library/91h11hx6.aspx
         * GlobalMultiUse: Similar to Shared/Static class, but class name is NOT needed to call function.
         * PublicNotCreatable: Public class with friend constructor: http://www.vbmigration.com/detknowledgebase.aspx?Id=281
         * Private: Private class
         * MultiUse: Public class with public constructor
         * 
         */

        public ClassInstancingType InstancingType { get; internal set; }

        internal VBClassModule(VisualBasic6Lexer vb6Lexer, VisualBasic6Parser vb6Parser, CommonTokenStream commonTokenStream, ParserRuleContext context) : base(vb6Lexer, vb6Parser, commonTokenStream, context) { }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            writer.Write($"Public Class {ModuleName}");
            writer.Indent();
            foreach (var model in CodeModels)
            {
                model.Convert(writer);
            }
            writer.Outdent();
            writer.NewLine();
            writer.WriteLine("End Class");
        }
    }
}
