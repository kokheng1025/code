using System;
using System.Linq;

using Antlr4.Runtime;
//using VB6ActiveXDllConverter.Scanner;

namespace VB6ActiveXDllConverter.Parsers
{
    public class VisualBasic6ErrorListener : BaseErrorListener
    {
        //public override void ReportAmbiguity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, bool exact, BitSet ambigAlts, ATNConfigSet configs)
        //{
        //    base.ReportAmbiguity(recognizer, dfa, startIndex, stopIndex, exact, ambigAlts, configs);
        //}

        //public override void ReportAttemptingFullContext(Parser recognizer, DFA dfa, int startIndex, int stopIndex, BitSet conflictingAlts, SimulatorState conflictState)
        //{
        //    base.ReportAttemptingFullContext(recognizer, dfa, startIndex, stopIndex, conflictingAlts, conflictState);
        //}

        //public override void ReportContextSensitivity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, int prediction, SimulatorState acceptState)
        //{
        //    base.ReportContextSensitivity(recognizer, dfa, startIndex, stopIndex, prediction, acceptState);
        //}

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        }
    }
}