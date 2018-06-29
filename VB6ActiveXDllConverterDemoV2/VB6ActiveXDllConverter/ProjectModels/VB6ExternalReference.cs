using System;
using System.Linq;

namespace VB6ActiveXDllConverter.ProjectModels
{
    public class VB6ExternalReference
    {
        public string ProcedureName { get; internal set; }
        public string LibraryName { get; internal set; }
        public string Alias { get; internal set; }

        internal VB6ExternalReference()
        {
            //
        }
    }
}
