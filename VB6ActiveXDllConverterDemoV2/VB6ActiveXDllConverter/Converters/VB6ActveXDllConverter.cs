using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ProjectModels;

namespace VB6ActiveXDllConverter.Converters
{
    public class VB6ActveXDllConverter
    {
        public void Convert(string vb6ProjectFilePath, string dotNetFolderPath)
        {
            string originalWorkingDirectory = string.Empty;
            try
            {
                originalWorkingDirectory = Directory.GetCurrentDirectory();

                // Extract data from VB6 project file and convert it to .net project file.
                var projectParser = new VisualBasic6ProjectParser();
                var vb6Project = projectParser.ParseProjectFile(vb6ProjectFilePath);
                var vbNetProject = new VBNetProject(vb6Project);
                var vbNetWriter = new VBNetProjectFileWriter(vbNetProject, originalWorkingDirectory, dotNetFolderPath);
                vbNetWriter.CreateProjectFile();

                var vb6ProjectFolder = Path.GetDirectoryName(vb6ProjectFilePath);
                foreach (var md in vb6Project.ProjectModules)
                    ProcessFile(vb6ProjectFolder, vbNetWriter, md);

                foreach (var cls in vb6Project.ProjectClasses)
                    ProcessFile(vb6ProjectFolder, vbNetWriter, cls);
            }
            catch (Exception ex)
            {
                // Debugging purposes...
                throw;
            }
            //finally
            //{
            //    if (!string.IsNullOrWhiteSpace(originalWorkingDirectory))
            //        Directory.SetCurrentDirectory(originalWorkingDirectory);
            //}
        }

        private void ProcessFile(string vb6ProjectFolder, VBNetProjectFileWriter vbNetWriter, VB6SourceFile codeFile)
        {

            var fileToConvert = Path.Combine(vb6ProjectFolder, codeFile.SourceFileName);
            if (File.Exists(fileToConvert))
            {
                var dotNetFileName = Path.Combine(vbNetWriter.ProjectFolder, Path.GetFileNameWithoutExtension(codeFile.SourceFileName) + ".vb");
                ConvertCodeFile(codeFile, fileToConvert, dotNetFileName);
            }
            else
            {
                throw new Exception($"VB6 code file not found: {codeFile.SourceFileName}");
            }
        }

        private void ConvertCodeFile(VB6SourceFile vb6File, string vb6CodeFilePath, string vbNetCodeFilePath)
        {
            var sr = new StreamReader(vb6CodeFilePath);
            var source = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();

            var processedCode = PreprocessCodeFile(source);
            var lineCount = processedCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;

            AntlrInputStream inputStream = new AntlrInputStream(processedCode);
            VisualBasic6Lexer vb6Lexer = new VisualBasic6Lexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(vb6Lexer); // TokenConstants.HiddenChannel
            VisualBasic6Parser vb6Parser = new VisualBasic6Parser(commonTokenStream);
            vb6Parser.AddErrorListener(new VisualBasic6ErrorListener());

            // Parse the file.
            var ctx = vb6Parser.module();

            if ((lineCount != vb6Lexer.Line) || (vb6Parser.NumberOfSyntaxErrors > 0))
                throw new Exception($"Total lines: [{lineCount}] Parsed lines: [{vb6Lexer.Line}], NumberOfSyntaxErrors: [{vb6Parser.NumberOfSyntaxErrors}]");

            var codeModelFactory = new VB6ModuleCodeModelFactory(vb6File.SourceFileType, vb6Lexer, vb6Parser, commonTokenStream);
            var model = codeModelFactory.CreateModuleCodeModels(ctx);

            // Convert to VB.Net
            var writer = new VBNetCodeFileWriter(vbNetCodeFilePath);
            writer.BeginWrite();

            model.Convert(writer);
            writer.EndWrite();

            ctx = null;
            source = null;
            processedCode = null;
            inputStream = null;
            commonTokenStream = null;
            vb6Lexer = null;
            vb6Parser = null;
        }

        private string PreprocessCodeFile(string codeBlock)
        {
            var sr = new StringReader(codeBlock);
            var sw = new StringWriter();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // Note: This one doesn't fix 100% all problems, like nested If-Then-Else in one line...
                //       Maybe exclude "ElseIf", since colon is valid if it is on another line, but this will still trigger parser error if didn't handle:
                //       ElseIf Not m_bVisibleExtra(i) Then: Exit For
                #region Auto fix COLON issues

                if (line.EndsWith("Then:", StringComparison.InvariantCultureIgnoreCase))
                {
                    // #2: Just " End If" and treat it as blank If..Then statement
                    line += " End If";
                }
                //else if (line.Contains("Then:"))
                else if (line.IndexOf("Then:", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    // #3: There is additional text after "Then:" if it come to this branch...
                    var iThen = line.IndexOf("Then:", StringComparison.InvariantCultureIgnoreCase);
                    line = line.Remove(iThen + 4, 1); // Remove the COLON from THEN

                }

                //// Note: Cannot fix like this, it will cause line label to be interpreted as method call
                //else if (line.EndsWith(":"))
                //{
                //    // #1: Just add a space
                //    line += " ";
                //}

                //// Note: Cannot fix like, since the COLON after ELSE is needed if it is on differet line than IF.
                //// Also will accidentally change CASE ELSE:
                //if (line.IndexOf("Else:", StringComparison.InvariantCultureIgnoreCase) > 0)
                //{
                //    // #3: Assumed: ELSE is always at middle of line, not the end. Always have statement after ELSE.
                //    var iElse = line.IndexOf("Else:", StringComparison.InvariantCultureIgnoreCase);
                //    line = line.Remove(iElse + 4, 1); // Remove the COLON from THEN
                //}

                #endregion

                sw.WriteLine(line);
            }

            sw.Flush();
            var result = sw.GetStringBuilder().ToString();
            sr.Close();
            sr.Dispose();
            sw.Close();
            sw.Dispose();
            sr = null;
            sw = null;

            return result;
        }
    }
}
