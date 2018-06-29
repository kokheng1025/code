using System;
using System.IO;
using System.Linq;
using System.Globalization;

using VB6ActiveXDllConverter.ProjectModels;

namespace VB6ActiveXDllConverter.Parsers
{
    public class VisualBasic6ProjectParser
    {
        private VB6Project _vb6Project;

        public VB6Project ParseProjectFile(string projectFilePath)
        {
            StreamReader sr = null;
            try
            {
                _vb6Project = new VB6Project();
                _vb6Project.ProjectFileName = Path.GetFileName(projectFilePath);

                // Note: This would assumed all source code have unique folder name, which is the case in Globe. Just using this as logical key to delete data.
                // Didn't handle scenario where same folder name exist in subfolder or even vbp file located at C:\Project1.vbp (These are not typical Globe scenario...)
                _vb6Project.ProjectFolderName = new DirectoryInfo(projectFilePath).Parent.Name;

                string line;
                sr = new StreamReader(projectFilePath);
                while ((line = sr.ReadLine()) != null)
                {
                    Parse(line);
                }

                return _vb6Project;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        private void Parse(string line)
        {
            // Note: Very poor man parser, only hardcoded to parse certain keywords...
            if (line.StartsWith("Type", true, CultureInfo.InvariantCulture))
                ParseType(line);

            else if (line.StartsWith("Name", true, CultureInfo.InvariantCulture))
                _vb6Project.ProjectName = ParseNameValuePair(line, '=');

            else if (line.StartsWith("Title", true, CultureInfo.InvariantCulture))
                _vb6Project.ProjectTitle = ParseNameValuePair(line, '=');

            else if (line.StartsWith("ExeName32", true, CultureInfo.InvariantCulture))
                _vb6Project.TargetName = ParseNameValuePair(line, '=');

            else if (line.StartsWith("MajorVer", true, CultureInfo.InvariantCulture))
                _vb6Project.MajorVer = int.Parse(ParseNameValuePair(line, '='));

            else if (line.StartsWith("MinorVer", true, CultureInfo.InvariantCulture))
                _vb6Project.MinorVer = int.Parse(ParseNameValuePair(line, '='));

            else if (line.StartsWith("RevisionVer", true, CultureInfo.InvariantCulture))
                _vb6Project.RevisionVer = int.Parse(ParseNameValuePair(line, '='));

            else if (line.StartsWith("Unattended", true, CultureInfo.InvariantCulture))
                _vb6Project.Unattended = (ParseNameValuePair(line, '=') == "0") ? false : true;

            else if (line.StartsWith("Retained", true, CultureInfo.InvariantCulture))
                _vb6Project.Retained = (ParseNameValuePair(line, '=') == "0") ? false : true;

            else if (line.StartsWith("ThreadingModel", true, CultureInfo.InvariantCulture))
                _vb6Project.ThreadingModel = (ParseNameValuePair(line, '=') == "1") ? ThreadingModelEnum.ApartmentThreaded : ThreadingModelEnum.SingleThreaded;

            else if (line.StartsWith("ThreadPerObject", true, CultureInfo.InvariantCulture))
                _vb6Project.ThreadPerObject = (ParseNameValuePair(line, '=') == "0") ? false : true;

            else if (line.StartsWith("MaxNumberOfThreads", true, CultureInfo.InvariantCulture))
                _vb6Project.MaxNumberOfThreads = int.Parse(ParseNameValuePair(line, '='));

            else if (line.StartsWith("Reference", true, CultureInfo.InvariantCulture))
                _vb6Project.AddProjectReference(ParseReference(line));

            else if (line.StartsWith("Object", true, CultureInfo.InvariantCulture))
                _vb6Project.AddObjectReference(ParseReference(line));

            else if (line.StartsWith("Module", true, CultureInfo.InvariantCulture))
                _vb6Project.AddStandardModule(ParseCodeFile(line));

            else if (line.StartsWith("Class", true, CultureInfo.InvariantCulture))
                _vb6Project.AddClassModule(ParseCodeFile(line));

            else if (line.StartsWith("Form", true, CultureInfo.InvariantCulture))
                _vb6Project.AddFormModule(ParseCodeFile(line));

            else if (line.StartsWith("UserControl", true, CultureInfo.InvariantCulture))
                _vb6Project.AddUserControl(ParseCodeFile(line));

            else if (line.StartsWith("PropertyPage", true, CultureInfo.InvariantCulture))
                _vb6Project.AddPropertyPage(ParseCodeFile(line));
        }

        private void ParseType(string line)
        {
            var data = line.Split('=');
            switch (data[1])
            {
                case "OleDll":
                    _vb6Project.ProjectType = ProjectTypeEnum.ActiveXDll;
                    break;
                case "OleExe":
                    _vb6Project.ProjectType = ProjectTypeEnum.ActiveXExe;
                    break;
                case "Exe":
                    _vb6Project.ProjectType = ProjectTypeEnum.StandardExe;
                    break;
                case "Control":
                    _vb6Project.ProjectType = ProjectTypeEnum.Control;
                    break;
                default:
                    _vb6Project.ProjectType = ProjectTypeEnum.Unknown;
                    break;
            }
        }

        private string ParseNameValuePair(string line, char delimiter)
        {
            var data = line.Split(delimiter);
            return data[1].Trim().Replace("\"",string.Empty); // Name Value pair, only return the value. Also remove extra double quote from string literals.
        }

        private VB6ProjectReference ParseReference(string line)
        {
            if (line.StartsWith("Reference", true, CultureInfo.InvariantCulture))
                return ParseProjectReference(line);
            else if (line.StartsWith("Object", true, CultureInfo.InvariantCulture))
                return ParseObjectReference(line);
            else
                throw new InvalidOperationException($"Unknown project reference line: {line}");
        }

        private VB6ProjectReference ParseProjectReference(string line)
        {
            // Reference=*\G{00020430-0000-0000-C000-000000000046}#2.0#0#..\..\Windows\system32\stdole2.tlb#OLE Automation

            var result = new VB6ProjectReference();
            var nameValuePair = line.Split('=');
            var data = nameValuePair[1].Split('#'); // first 3 is type lib, fourth one is file path, fifth is optional description.

            result.ReferenceType = ProjectReferenceTypeEnum.ProjectReference;
            result.TypeLibID = new string(data[0].Skip(3).ToArray()); // Get GUID
            result.TypeLibVer = data[1];
            result.TypeLibLCID = data[2];
            result.TypeLibFileName = data[3];

            if (data.Length > 4)
                result.TypeLibDescription = data[4];

            return result;
        }

        private VB6ProjectReference ParseObjectReference(string line)
        {
            // Object={831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0; mscomctl.OCX

            var result = new VB6ProjectReference();
            var nameValuePair = line.Split('=');
            var data = nameValuePair[1].Split(';');
            var tlbRef = data[0].Split('#');

            result.ReferenceType = ProjectReferenceTypeEnum.ObjectReference;
            result.TypeLibFileName = data[1].Trim();

            if (tlbRef.Length == 1)
            {
                // "Object=Exact.ExactMenu.1; e4slax.dll"
                result.TypeLibID = tlbRef[0];
                result.TypeLibVer = null;
                result.TypeLibLCID = null;
            }
            else
            {
                result.TypeLibID = tlbRef[0];
                result.TypeLibVer = tlbRef[1];
                result.TypeLibLCID = tlbRef[2];
            }

            return result;
        }

        private VB6SourceFile ParseCodeFile(string line)
        {
            // Seems like only Module & Class contain title:
            // Module=Module1; Module1.bas
            // Class=Class1; Class1.cls
            // Form=Form1.frm
            // UserControl=UserControl1.ctl
            // PropertyPage=PropertyPage1.pag

            var result = new VB6SourceFile();
            var data = line.Split('=');

            switch (data[0])
            {
                case "Module":
                    result.SourceFileType = SourceFileTypeEnum.Module;
                    break;
                case "Class":
                    result.SourceFileType = SourceFileTypeEnum.Class;
                    break;
                case "Form":
                    result.SourceFileType = SourceFileTypeEnum.Form;
                    break;
                case "UserControl":
                    result.SourceFileType = SourceFileTypeEnum.UserControl;
                    break;
                case "PropertyPage":
                    result.SourceFileType = SourceFileTypeEnum.PropertyPage;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown project code file type: {line}");
            }

            if ((result.SourceFileType == SourceFileTypeEnum.Module) || (result.SourceFileType == SourceFileTypeEnum.Class))
            {
                var subData = data[1].Split(';');
                result.Title = subData[0].Trim();
                result.SourceFileName = subData[1].Trim();
            }
            else
            {
                result.SourceFileName = data[1].Trim();
            }

            return result;
        }
    }
}
