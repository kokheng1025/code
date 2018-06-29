using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBBaseModule : VBBaseCodeModel
    {
        internal VisualBasic6Lexer VB6Lexer { get; }
        internal VisualBasic6Parser VB6Parser { get; }
        internal CommonTokenStream VB6CommonTokenStream { get; }
        internal int LastScanTokenIndex { get; set; } = -1; // Keep track of last position where generic scanning stopped.
        internal int LastCommentScanTokenIndex {get; set; } = -1; // Keep track of last position where comment scanning stopped.

        public string ModuleName { get; internal set; }

        private List<ConversionMessageBase> _conversionMessageList = new List<ConversionMessageBase>();
        public ReadOnlyCollection<ConversionMessageBase> ConversionMessageList { get; }


        // For Demo
        public Dictionary<string, VBPropertyGetStmt> PropsGet { get; } = new Dictionary<string, VBPropertyGetStmt>();
        public Dictionary<string, VBPropertyLetStmt> PropsLet { get; } = new Dictionary<string, VBPropertyLetStmt>();
        public Dictionary<string, VBPropertySetStmt> PropsSet { get; } = new Dictionary<string, VBPropertySetStmt>();


        internal VBBaseModule(VisualBasic6Lexer vb6Lexer, VisualBasic6Parser vb6Parser, CommonTokenStream commonTokenStream, ParserRuleContext context) : base(context)
        {
            VB6Lexer = vb6Lexer;
            VB6Parser = vb6Parser;
            VB6CommonTokenStream = commonTokenStream;
            Scope = new VBScope(null, this, VBProgramScope.Module);
            ConversionMessageList = new ReadOnlyCollection<ConversionMessageBase>(_conversionMessageList);
        }

        internal void AddConversionMessage(ConversionMessageBase info)
        {
            _conversionMessageList.Add(info);
        }
    }
}
