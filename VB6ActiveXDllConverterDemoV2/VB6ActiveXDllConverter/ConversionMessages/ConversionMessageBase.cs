using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class ConversionMessageBase
    {
        public string Id { get; internal set; }
        public int Line { get; internal set; }
        public int Column { get; internal set; }
        public string Description { get; internal set; }
        public string RequiredAction { get; internal set; }

        internal ConversionMessageBase() { }
    }
}
