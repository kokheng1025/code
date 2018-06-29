using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedFixedLengthString : ConversionMessageBase
    {
        internal UnSupportedFixedLengthString(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedFixedLengthString";
            Description = "Fixed length string declaration is not support in VB.Net";
            RequiredAction = "String is converted to variable length without space padding. Manually analyze the code to check for any impact.";
        }
    }
}
