using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedSavePicture : ConversionMessageBase
    {
        internal UnSupportedSavePicture(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedSavePicture";
            Description = "SavePicture is not support in VB.Net";
            RequiredAction = "Manually replace the function in .Net, such as using Image.Save or other UI control specific function.";
        }
    }
}
