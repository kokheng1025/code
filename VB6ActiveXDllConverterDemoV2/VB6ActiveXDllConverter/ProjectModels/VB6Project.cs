using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VB6ActiveXDllConverter.ProjectModels
{
    public enum ProjectTypeEnum
    {
        Unknown,
        ActiveXDll,
        ActiveXExe,
        StandardExe,
        Control
    }

    public enum ThreadingModelEnum
    {
        SingleThreaded,
        ApartmentThreaded
    }

    public class VB6Project
    {
        public string ProjectFolderName { get; internal set; }
        public string ProjectFileName { get; internal set; }
        public ProjectTypeEnum ProjectType { get; internal set; }
        public string ProjectName { get; internal set; }
        public string ProjectTitle { get; internal set; }
        public string TargetName { get; internal set; }
        public int MajorVer { get; internal set; }
        public int MinorVer { get; internal set; }
        public int RevisionVer { get; internal set; }
        public bool Unattended { get; internal set; }
        public bool Retained { get; internal set; }

        // Explaination on VB6 threading model- https://msdn.microsoft.com/en-us/LIbrary/aa261361(v=vs.60).aspx
        // If Apartment Threaded setting is turned on in project setting, then ThreadingModel=1 should exist in project file.
        // If Single Threaded is turned on, then ThreadingModel setting is missing in ActiveX DLL vbp, therefore it is defaulted to 0.
        public ThreadingModelEnum ThreadingModel { get; internal set; } = ThreadingModelEnum.SingleThreaded;

        public bool ThreadPerObject { get; internal set; }
        public int MaxNumberOfThreads { get; internal set; }

        private List<VB6ProjectReference> _projectReferences = new List<VB6ProjectReference>();
        public ReadOnlyCollection<VB6ProjectReference> ProjectReferences { get; }

        private List<VB6ProjectReference> _objectReferences = new List<VB6ProjectReference>();
        public ReadOnlyCollection<VB6ProjectReference> ObjectReferences { get; }

        private List<VB6SourceFile> _classes = new List<VB6SourceFile>();
        public ReadOnlyCollection<VB6SourceFile> ProjectClasses { get; }

        private List<VB6SourceFile> _modules = new List<VB6SourceFile>();
        public ReadOnlyCollection<VB6SourceFile> ProjectModules { get; }

        private List<VB6SourceFile> _forms = new List<VB6SourceFile>();
        public ReadOnlyCollection<VB6SourceFile> ProjectForms { get; }

        private List<VB6SourceFile> _userControls = new List<VB6SourceFile>();
        public ReadOnlyCollection<VB6SourceFile> ProjectUserControls { get; }

        private List<VB6SourceFile> _propertyPages = new List<VB6SourceFile>();
        public ReadOnlyCollection<VB6SourceFile> ProjectPropertyPages { get; }

        private List<VB6ParseError> _errors = new List<VB6ParseError>();
        public ReadOnlyCollection<VB6ParseError> Errors { get; }

        internal VB6Project()
        {
            // Readonly collection can be created once, instead of creating new object everytime GET property is called.
            ProjectReferences = new ReadOnlyCollection<VB6ProjectReference>(_projectReferences);
            ObjectReferences = new ReadOnlyCollection<VB6ProjectReference>(_objectReferences);
            ProjectClasses = new ReadOnlyCollection<VB6SourceFile>(_classes);
            ProjectModules = new ReadOnlyCollection<VB6SourceFile>(_modules);
            ProjectForms = new ReadOnlyCollection<VB6SourceFile>(_forms);
            ProjectUserControls = new ReadOnlyCollection<VB6SourceFile>(_userControls);
            ProjectPropertyPages = new ReadOnlyCollection<VB6SourceFile>(_propertyPages);
            Errors = new ReadOnlyCollection<VB6ParseError>(_errors);
        }

        internal void AddProjectReference(VB6ProjectReference projectReference)
        {
            _projectReferences.Add(projectReference);
        }

        internal void AddObjectReference(VB6ProjectReference objectReference)
        {
            _objectReferences.Add(objectReference);
        }

        internal void AddClassModule(VB6SourceFile classModule)
        {
            _classes.Add(classModule);
        }

        internal void AddStandardModule(VB6SourceFile standardModule)
        {
            _modules.Add(standardModule);
        }

        internal void AddFormModule(VB6SourceFile formModule)
        {
            _forms.Add(formModule);
        }

        internal void AddUserControl(VB6SourceFile userControl)
        {
            _userControls.Add(userControl);
        }

        internal void AddPropertyPage(VB6SourceFile propertyPage)
        {
            _propertyPages.Add(propertyPage);
        }

        internal void AddError(string errorMessage)
        {
            var e = new VB6ParseError();
            e.ErrorMessage = errorMessage;
            _errors.Add(e);
        }
    }
}
