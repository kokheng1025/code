using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ConversionMessages
{
    public class UnSupportedClassTerminate : ConversionMessageBase
    {
        internal UnSupportedClassTerminate(int line, int col) : base()
        {
            Line = line;
            Column = col;

            Id = "UnSupportedClassTerminate";
            Description = "Class_Terminate cannot be converted with 100% identical behavior. VB.net Finalize is not executed immediately after all references are cleared.";
            RequiredAction = "Manual refactoring is needed if critical tasks are done, like dropping temp table/view.";
        }
    }
}
