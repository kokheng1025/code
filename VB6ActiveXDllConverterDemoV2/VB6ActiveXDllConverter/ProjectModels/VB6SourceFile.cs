using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VB6ActiveXDllConverter.ProjectModels
{
    public enum SourceFileTypeEnum
    {
        Unknown,
        Class,
        Module,
        UserControl,
        Form,
        PropertyPage
    }

    public class VB6SourceFile
    {
        public string SourceFileName { get; internal set; }
        public SourceFileTypeEnum SourceFileType { get; internal set; } = SourceFileTypeEnum.Unknown;
        public string Title { get; internal set; }

        internal VB6SourceFile() { }
    }
}
