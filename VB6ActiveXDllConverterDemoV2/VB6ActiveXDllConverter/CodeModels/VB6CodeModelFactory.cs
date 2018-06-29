using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using VB6ActiveXDllConverter.Parsers;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ProjectModels;
using VB6ActiveXDllConverter.CodeModels;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VB6CodeModelFactory
    {
        private VBBaseModule _rootModule; 
        private VBBaseCodeModel _parentCodeBlock;

        public VB6CodeModelFactory(VBBaseModule rootModule, VBBaseCodeModel parentCodeBlock)
        {
            _rootModule = rootModule;
            _parentCodeBlock = parentCodeBlock;
        }

        public VBBaseCodeModel CreateCodeModel(ParserRuleContext context)
        {
            try
            {
                // Create code model based on context name
                // e.g: DeclareStmtContext will create VBDeclareStmt class
                var contextClassName = context.GetType().Name;
                var codeModelClassName = "VB" + contextClassName.Replace("Context", string.Empty);

                var cxt = new VB6CodeModelFactoryContext(_rootModule, _parentCodeBlock, context, _parentCodeBlock.Scope);
                var args = new object[] { cxt };
                var t = Type.GetType($"VB6ActiveXDllConverter.CodeModels.{codeModelClassName}", true);
                var obj = (VBBaseCodeModel)t.Assembly.CreateInstance($"VB6ActiveXDllConverter.CodeModels.{codeModelClassName}", true, BindingFlags.Instance | BindingFlags.NonPublic, null, args, null, null);

                obj.BeforeVisitChild();
                _parentCodeBlock.AddCodeModel(obj);
                obj.VisitChild();
                obj.AfterVisitChild();

                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public VBText CreateCodeModel(ITerminalNode node)
        {
            try
            {
                var cxt = new VB6CodeModelFactoryContext(_rootModule, _parentCodeBlock, null, _parentCodeBlock.Scope);
                var obj = new VBText(cxt, node);

                obj.BeforeVisitChild();
                _parentCodeBlock.AddCodeModel(obj);
                obj.AfterVisitChild();

                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
