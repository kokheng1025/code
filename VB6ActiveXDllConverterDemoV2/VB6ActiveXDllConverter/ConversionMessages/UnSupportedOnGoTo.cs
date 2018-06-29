using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedOnGoTo : ConversionMessageBase
    {
        internal UnSupportedOnGoTo(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedOnGoTo";
            Description = "On...GoTo statement is not support in VB.Net";
            RequiredAction = "Manually refactor the code into Select Case or other structure.";
        }
    }
}
