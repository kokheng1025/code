using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedNonZeroOptionBase : ConversionMessageBase
    {
        internal UnSupportedNonZeroOptionBase(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedNonZeroOptionBase";
            Description = "Option Base with non-zero number is detected. This is not support in VB.Net";
            RequiredAction = "Manually adjust array indexes in source code to become zero base.";
        }
    }
}
