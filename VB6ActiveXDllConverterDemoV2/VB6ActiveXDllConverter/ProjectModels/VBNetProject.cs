using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.ProjectModels
{
    // Only support limited project types
    public enum DotNetProjectTypeEnum
    {
        Unknown,
        ClassLibrary, // Library
        WinFormApp, // WinExe
        ConsoleApp // Exe
    }

    public class VBNetProject
    {
        public VB6Project OriginalVBProject { get; internal set; }
        public string ProjectFolderName { get; internal set; }
        public string ProjectFileName { get; internal set; }
        public DotNetProjectTypeEnum ProjectType { get; internal set; }
        public Guid ProjectGuid { get; internal set; }
        public string ProjectTitle { get; internal set; }
        public string TargetName { get; internal set; }
        public string DocumentationFileName { get; internal set; }
        public int MajorVer { get; internal set; }
        public int MinorVer { get; internal set; }
        public int RevisionVer { get; internal set; }

        private List<string> _classes = new List<string>();
        public ReadOnlyCollection<string> ProjectClasses { get; }

        private List<string> _modules = new List<string>();
        public ReadOnlyCollection<string> ProjectModules { get; }

        internal VBNetProject(VB6Project vb6Project)
        {
            OriginalVBProject = vb6Project;
            ProjectClasses = new ReadOnlyCollection<string>(_classes);
            ProjectModules = new ReadOnlyCollection<string>(_modules);
            MapProjectProps();
        }

        private void MapProjectProps()
        {
            if (OriginalVBProject.ProjectType != ProjectTypeEnum.ActiveXDll)
                throw new InvalidOperationException("Only VB6 ActiveX DLL project is supported");

            ProjectGuid = Guid.NewGuid();
            ProjectType = DotNetProjectTypeEnum.ClassLibrary;
            ProjectTitle = OriginalVBProject.ProjectTitle;
            ProjectFolderName = OriginalVBProject.ProjectFolderName;
            ProjectFileName = Path.GetFileNameWithoutExtension(OriginalVBProject.ProjectFileName) + ".vbproj";
            TargetName = Path.GetFileNameWithoutExtension(OriginalVBProject.TargetName);
            DocumentationFileName = Path.GetFileNameWithoutExtension(OriginalVBProject.TargetName) + ".xml";
            MajorVer = OriginalVBProject.MajorVer;
            MinorVer = OriginalVBProject.MinorVer;
            RevisionVer = OriginalVBProject.RevisionVer;

            foreach (var cls in OriginalVBProject.ProjectClasses)
                _classes.Add(Path.GetFileNameWithoutExtension(cls.SourceFileName) + ".vb");

            foreach (var md in OriginalVBProject.ProjectModules)
                _modules.Add(Path.GetFileNameWithoutExtension(md.SourceFileName) + ".vb");
        }
    }
}
