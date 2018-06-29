using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedGoSubReturn : ConversionMessageBase
    {
        internal UnSupportedGoSubReturn(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedGoSubReturn";
            Description = "GoSub...Return statement is not support in VB.Net";
            RequiredAction = "Manually refactor the code into different sub/function";
        }
    }
}
