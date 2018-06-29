using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedCurrencyType : ConversionMessageBase
    {
        internal UnSupportedCurrencyType(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedCurrencyType";
            Description = "Currency data type is not support in VB.Net";
            RequiredAction = "Manually replace the variable type. Can use Decimal in .net, but the data size is larger. Need testing to verify behavior.";
        }
    }
}
