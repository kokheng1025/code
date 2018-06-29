using System;
using System.Linq;

namespace VB6ActiveXDllConverter.ProjectModels
{
    public class VB6GrammarIndex
    {
        public int RuleId { get; internal set; }
        public int LineNumber { get; internal set; }
        public int ColumnNumber { get; internal set; }
        public string GrammarData { get; internal set; }

        internal VB6GrammarIndex()
        {
            //
        }
    }
}
