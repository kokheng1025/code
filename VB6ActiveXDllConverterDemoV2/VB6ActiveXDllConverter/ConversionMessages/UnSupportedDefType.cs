using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedDefType : ConversionMessageBase
    {
        internal UnSupportedDefType(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedDefType";
            Description = "DefType statements are not support in VB.Net";
            RequiredAction = "DefBool, DefInt, DefLng and others are not supported. Manually fill in data type for variables, argument passed to procedures, and the return type for Function and PropertyGet procedures whose names start with the specified characters.";
        }
    }
}
