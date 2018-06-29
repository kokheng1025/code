using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.Converters
{
    internal class VBNetCodeFileWriter
    {
        private const int INDENT_SPACES = 4;

        private StringBuilder _sb;
        private int _indentLevel = 0;
        private bool _suppressIndent = false;

        public string DestinationFilePath { get; }

        internal VBNetCodeFileWriter(string destinationFilePath)
        {
            DestinationFilePath = destinationFilePath;
        }

        public void BeginWrite()
        {
            // Kick start the writer. 
            _sb = new StringBuilder();
        }

        public void EndWrite()
        {
            // Flush all content to file, close it...
            File.WriteAllText(DestinationFilePath, _sb.ToString());
        }

        public void Indent()
        {
            _indentLevel += INDENT_SPACES;
        }

        public void Outdent()
        {
            _indentLevel -= INDENT_SPACES;
            if (_indentLevel < 0) throw new InvalidOperationException("Indentation becomes -ve!");
        }

        public void NewLine()
        {
            _sb.AppendLine();
        }

        // Temporary do not write any indentation. This is needed when code exist in inline style. Like inline If-Then-Else.
        public void SuppressIndent()
        {
            _suppressIndent = true;
        }

        public void UnsuppressIndent()
        {
            _suppressIndent = false;
        }

        public void WriteIndent()
        {
            if (!_suppressIndent)
                _sb.Append(GetIndentString());
        }

        public void Write(string content) //, bool withIndentation = false)
        {
            _sb.Append(content);
        }

        public void WriteLine(string content)
        {
            // Add new line at the end
            _sb.AppendLine(content);
        }

        private string GetIndentString()
        {
            return new string(' ', _indentLevel);
        }
    }
}
