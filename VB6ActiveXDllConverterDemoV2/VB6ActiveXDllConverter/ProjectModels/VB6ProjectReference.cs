using System;
using System.Linq;

namespace VB6ActiveXDllConverter.ProjectModels
{
    public enum ProjectReferenceTypeEnum
    {
        Unknown,
        ProjectReference,
        ObjectReference
    }

    public class VB6ProjectReference
    {
        public ProjectReferenceTypeEnum ReferenceType { get; internal set; }
        public string TypeLibID { get; internal set; }
        public string TypeLibVer { get; internal set; }
        public string TypeLibLCID { get; internal set; }
        public string TypeLibFileName { get; internal set; }
        public string TypeLibDescription { get; internal set; }

        internal VB6ProjectReference()
        {
            //
        }
    }
}
