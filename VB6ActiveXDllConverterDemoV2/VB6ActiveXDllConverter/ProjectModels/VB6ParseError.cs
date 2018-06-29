using System;
using System.Linq;

namespace VB6ActiveXDllConverter.ProjectModels
{
    public class VB6ParseError
    {
        public string NodeText { get; internal set; }
        public string ParentNodeText { get; internal set; }
        public int? LineNumber { get; internal set; }
        public int? ColumnNumber { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }
}
