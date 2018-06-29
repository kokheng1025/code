using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

using VB6ActiveXDllConverter.ProjectModels;

namespace VB6ActiveXDllConverter.Converters
{
    internal class VBNetProjectFileWriter
    {
        private const string TEMPLATE_CLASS = @"Templates\ClassLibraryProject.txt";
        private const string TEMPLATE_CONSOLE = @"Templates\ConsoleAppProject.txt";
        private const string TEMPLATE_WINFORM = @"Templates\WindowsAppProject.txt";
        private const string TEMPLATE_ASMINFO = @"Templates\AssemblyInfo.txt";

        private VBNetProject _vbNetProject;
        private string _currentworkingFolder;

        public string ProjectFolder { get; internal set; }

        internal VBNetProjectFileWriter(VBNetProject vbNetProject, string workingFolderPath, string destFolderPath)
        {
            _vbNetProject = vbNetProject;
            _currentworkingFolder = workingFolderPath;
            ProjectFolder = Path.Combine(destFolderPath, vbNetProject.ProjectFolderName);
        }

        public void CreateProjectFile()
        {
            var root = ReadTemplate(_currentworkingFolder, _vbNetProject.ProjectType); 
            var propGroupNode = (from el in root.Elements(root.GetDefaultNamespace() + "PropertyGroup")
                                 where (string)el.Element(root.GetDefaultNamespace() + "ProjectGuid") == "[[ProjectGuid]]"
                                 select el).FirstOrDefault(); // Should found only one line.

            if (propGroupNode is null)
                throw new InvalidOperationException("Incorrect Template file. Cannot find [[ProjectGuid]] placeholder.");

            propGroupNode.Element(root.GetDefaultNamespace() + "ProjectGuid").Value = _vbNetProject.ProjectGuid.ToString("B");
            propGroupNode.Element(root.GetDefaultNamespace() + "RootNamespace").Value = "Exact.VB6"; // Todo: Pass in from UI....
            propGroupNode.Element(root.GetDefaultNamespace() + "AssemblyName").Value = _vbNetProject.TargetName;

            var docFileNodes = from el in root.Elements(root.GetDefaultNamespace() + "PropertyGroup")
                               where (string)el.Element(root.GetDefaultNamespace() + "DocumentationFile") == "[[DocumentationFile]]"
                               select el;

            if (docFileNodes is null)
                throw new InvalidOperationException("Incorrect Template file. Cannot find [[DocumentationFile]] placeholder.");

            foreach (var item in docFileNodes)
            {
                item.Element(root.GetDefaultNamespace() + "DocumentationFile").Value = _vbNetProject.DocumentationFileName;
            }

            AddCodeFiles(root);

            Directory.CreateDirectory(ProjectFolder);
            root.Save(Path.Combine(ProjectFolder, _vbNetProject.ProjectFileName));

            GenerateAssemblyInfo();

            // By default add 2 information txt file.
            File.CreateText(Path.Combine(ProjectFolder, "ToDo.txt"));
            File.CreateText(Path.Combine(ProjectFolder, "References.txt"));
        }

        private XElement ReadTemplate(string workingFolderPath, DotNetProjectTypeEnum projectType)
        {
            switch (projectType)
            {
                case DotNetProjectTypeEnum.ClassLibrary:
                    return XElement.Load(Path.Combine(workingFolderPath, TEMPLATE_CLASS));

                case DotNetProjectTypeEnum.WinFormApp:
                    return XElement.Load(Path.Combine(workingFolderPath, TEMPLATE_WINFORM));

                case DotNetProjectTypeEnum.ConsoleApp:
                    return XElement.Load(Path.Combine(workingFolderPath, TEMPLATE_CONSOLE));

                default:
                    throw new ArgumentException($"Unknown project type: {projectType}");
            }
        }

        private void AddCodeFiles(XElement root)
        {
            var itemGroupNode = (from el in root.Elements(root.GetDefaultNamespace() + "ItemGroup").Descendants()
                                 where (string)el.Attribute("Include") == "AssemblyInfo.vb"
                                 select el).FirstOrDefault(); // Should found only one line.

            if (itemGroupNode is null)
                throw new InvalidOperationException("Incorrect Template file. Cannot find ItemGroup placeholder to add source files.");

            foreach (var item in _vbNetProject.ProjectModules)
            {
                var elm = new XElement(root.GetDefaultNamespace() + "Compile", new XAttribute("Include", item));
                itemGroupNode.Parent.Add(elm);
            }
            foreach (var item in _vbNetProject.ProjectClasses)
            {
                var elm = new XElement(root.GetDefaultNamespace() + "Compile", new XAttribute("Include", item));
                itemGroupNode.Parent.Add(elm);
            }
        }

        private void GenerateAssemblyInfo()
        {
            // Very poor man templating, AssemblyInfo.vb is not that big...
            var result = new StringBuilder();
            var verString = $"{_vbNetProject.MajorVer}.{_vbNetProject.MinorVer}.0.{_vbNetProject.RevisionVer}";
            var allLines = File.ReadLines(Path.Combine(_currentworkingFolder, TEMPLATE_ASMINFO));
            foreach (var line in allLines)
            {
                if (line.IndexOf("[[AssemblyTitle]]", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    result.AppendLine(line.Replace("[[AssemblyTitle]]", _vbNetProject.ProjectTitle));
                    continue;
                }

                // Description is not added for now.
                //if (line.IndexOf("[[AssemblyDescription]]", StringComparison.InvariantCultureIgnoreCase) > 0)
                //{
                //    line.Replace("[[AssemblyDescription]]", "");
                //    continue;
                //}

                if (line.IndexOf("[[AssemblyGuid]]", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    result.AppendLine(line.Replace("[[AssemblyGuid]]", Guid.NewGuid().ToString("D")));
                    continue;
                }

                if (line.IndexOf("[[AssemblyVersion]]", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    result.AppendLine(line.Replace("[[AssemblyVersion]]", verString));
                    continue;
                }
                if (line.IndexOf("[[AssemblyFileVersion]]", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    result.AppendLine(line.Replace("[[AssemblyFileVersion]]", verString));
                    continue;
                }

                result.AppendLine(line);
            }

            if (result.Length == 0)
                throw new Exception("Failed to generate AssemblyInfo.vb");

            File.WriteAllText(Path.Combine(ProjectFolder, "AssemblyInfo.vb"), result.ToString());
        }
    }
}
