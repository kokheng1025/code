using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.ConversionMessages;

namespace VB6ActiveXDllConverter.CodeModels
{
    // Represent a conversion issue that may requires manual fixing.
    public class VBConversionMessage : VBBaseModel
    {
        public ConversionMessageBase ConversionMessage { get; }

        public VBConversionMessage(ConversionMessageBase info)
        {
            ConversionMessage = info;
        }

        internal override void Convert(VBNetCodeFileWriter writer)
        {
            writer.NewLine();
            writer.WriteIndent();
            writer.WriteLine($"' #ISSUE: {ConversionMessage.Id}.");
            writer.WriteIndent();
            writer.WriteLine($"' Description: {ConversionMessage.Description}");
            writer.WriteIndent();
            writer.WriteLine($"' Required action: {ConversionMessage.RequiredAction}");
        }
    }
}
