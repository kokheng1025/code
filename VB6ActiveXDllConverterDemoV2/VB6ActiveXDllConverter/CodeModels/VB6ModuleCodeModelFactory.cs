using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ProjectModels;
using VB6ActiveXDllConverter.CodeModels;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.Converters
{
    public class VB6ModuleCodeModelFactory
    {
        private VisualBasic6Lexer _vb6Lexer;
        private VisualBasic6Parser _vb6Parser;
        private CommonTokenStream _commonTokenStream;
        private SourceFileTypeEnum _fileType;

        private VBBaseModule _moduleCodeModel;
        public VBBaseModule ModuleCodeModel { get { return _moduleCodeModel; } }

        public VB6ModuleCodeModelFactory(SourceFileTypeEnum fileType, VisualBasic6Lexer vb6Lexer, VisualBasic6Parser vb6Parser, CommonTokenStream commonTokenStream)
        {
            _fileType = fileType;
            _vb6Lexer = vb6Lexer;
            _vb6Parser = vb6Parser;
            _commonTokenStream = commonTokenStream;
        }

        public VBBaseModule CreateModuleCodeModels(VisualBasic6Parser.ModuleContext context)
        {
            // Create & extract module level metadata
            _moduleCodeModel = CreateModule(_fileType, context);
            var moduleVisitor = new VB6ModuleVisitor(_moduleCodeModel, _vb6Lexer, _vb6Parser, _commonTokenStream);
            moduleVisitor.Visit(context);
            moduleVisitor.PostProcess();

            // Visit code blocks to create code object model for analysis & conversion.
            var codeModelVisitor = new VB6CodeModelVisitor(_moduleCodeModel, _moduleCodeModel, _vb6Lexer, _vb6Parser, _commonTokenStream);
            codeModelVisitor.Visit(context.moduleBody());
            codeModelVisitor.PostProcess();

            return _moduleCodeModel;
        }

        private VBBaseModule CreateModule(SourceFileTypeEnum fileType, VisualBasic6Parser.ModuleContext context)
        {
            VBBaseModule result;
            switch (fileType)
            {
                case SourceFileTypeEnum.Unknown:
                    throw new Exception("Unknown VB6 file type.");
                case SourceFileTypeEnum.Class:
                    result = new VBClassModule(_vb6Lexer, _vb6Parser, _commonTokenStream, context);
                    break;
                case SourceFileTypeEnum.Module:
                    result = new VBStandardModule(_vb6Lexer, _vb6Parser, _commonTokenStream, context);
                    break;
                case SourceFileTypeEnum.UserControl:
                    throw new NotImplementedException("UserControl");
                    //break;
                case SourceFileTypeEnum.Form:
                    throw new NotImplementedException("Form");
                    //break;
                case SourceFileTypeEnum.PropertyPage:
                    throw new NotImplementedException("PropertyPage");
                    //break;
                default:
                    throw new Exception("Unsupported VB6 file type.");
            }

            return result;
        }
    }
}
