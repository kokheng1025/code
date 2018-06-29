using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class AmbiguousDeclareTypeHint : ConversionMessageBase
    {
        internal AmbiguousDeclareTypeHint(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "AmbiguousDeclareTypeHint";
            Description = "External declaration with more than one type hint. Haven't seen this scenario yet, not sure what the syntax means... Can have 2 return type or type hint can be placed at 2 different place?";
            RequiredAction = "Check if the VB6 code is valid or handle it manually. May need to change the converter if this happen frequently.";
        }
    }
}
