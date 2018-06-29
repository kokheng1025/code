using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Parsers;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.CodeModels;
using VB6ActiveXDllConverter.ConversionMessages;

namespace VB6ActiveXDllConverter.Visitors
{
    internal class VB6ModuleVisitor : VisualBasic6BaseVisitor<object>
    {
        private VisualBasic6Lexer _vb6Lexer;
        private VisualBasic6Parser _vb6Parser;
        private CommonTokenStream _commonTokenStream;
        private VBBaseModule _rootModule;

        private bool _isClassGlobalNameSpace;
        private bool _isClassCreatable;
        private bool _isClassExposed;
        private bool _isMultiUse;

        internal VB6ModuleVisitor(VBBaseModule rootModule, VisualBasic6Lexer vb6Lexer, VisualBasic6Parser vb6Parser, CommonTokenStream commonTokenStream)
        {
            _rootModule = rootModule;
            _vb6Lexer = vb6Lexer;
            _vb6Parser = vb6Parser;
            _commonTokenStream = commonTokenStream;

            // Filter out VB6 generated comments. Just discard all comments until the last Attribute statement. (Look at VB6 source file with notepad instead of VB6 IDE. It is not shown in IDE.)
            var lastAttributeStmt = (from itm in _commonTokenStream.Get(0, _commonTokenStream.Size)
                                     where (itm.Type == VisualBasic6Parser.ATTRIBUTE)
                                     select itm).LastOrDefault();

            if (lastAttributeStmt != null)
            {
                // Note: This will get token position for the keyword "Attribute", not very accurate, but enough to start.
                _rootModule.LastScanTokenIndex= lastAttributeStmt.TokenIndex;
            }
        }

        #region Post processing

        public void PostProcess()
        {
            if (_rootModule is VBClassModule)
                SetupClassInstancing();
        }

        private void SetupClassInstancing()
        {
            var m = _rootModule as VBClassModule;
            
            // Can check by creating ActiveX DLL project, setting different class instancing type and look at the source code.
            if (_isClassGlobalNameSpace && _isClassCreatable && _isClassExposed)
            {
                m.InstancingType = ClassInstancingType.GlobalMultiUse;
            }
            else if (!_isClassGlobalNameSpace && _isClassCreatable && _isClassExposed)
            {
                m.InstancingType = ClassInstancingType.MultiUse;
            }
            else if (!_isClassGlobalNameSpace && !_isClassCreatable && _isClassExposed)
            {
                m.InstancingType = ClassInstancingType.PublicNotCreatable;
            }
            else if (!_isClassGlobalNameSpace && !_isClassCreatable && !_isClassExposed)
            {
                m.InstancingType = ClassInstancingType.Private;
            }
            else
            {
                throw new Exception("Unknown VB6 class instancing type.");
            }
        }

        #endregion

        #region Visitor methods

        #region Class Module Config

        public override object VisitModuleConfigElement([NotNull] VisualBasic6Parser.ModuleConfigElementContext context)
        {
            object result = null;
            var cfgName = context.ambiguousIdentifier().GetText();
            var cfgValue = context.literal().GetText();
            if (cfgName.Equals("MultiUse", StringComparison.InvariantCultureIgnoreCase))
            {
                _isMultiUse = ((cfgValue == "0") ? false : true);
            }
            else
            {
                result = base.VisitModuleConfigElement(context);
            }

            return result;
        }

        #endregion

        #region Module Attributes

        public override object VisitAttributeStmt([NotNull] VisualBasic6Parser.AttributeStmtContext context)
        {
            object result = null;
            var attName = context.implicitCallStmt_InStmt().GetText();
            var attValue = context.literal()[0].GetText(); // For this few attributes, there is only one value, no comma. Array should always have at least one item.
            switch (true)
            {
                case bool b when attName.Equals("VB_Name", StringComparison.InvariantCultureIgnoreCase):
                    _rootModule.ModuleName = context.literal()[0].GetText().Replace("\"", string.Empty);
                    break;

                case bool b when attName.Equals("VB_GlobalNameSpace", StringComparison.InvariantCultureIgnoreCase):
                    _isClassGlobalNameSpace = bool.Parse(attValue);
                    break;

                case bool b when attName.Equals("VB_Creatable", StringComparison.InvariantCultureIgnoreCase):
                    _isClassCreatable = bool.Parse(attValue);
                    break;

                case bool b when attName.Equals("VB_Exposed", StringComparison.InvariantCultureIgnoreCase):
                    _isClassExposed = bool.Parse(attValue);
                    break;

                default:
                    result = base.VisitAttributeStmt(context);
                    break;
            }

            return result;
        }

        #endregion

        #region Module Options

        /*
         * Note on Option:
         * - Option Base 1: Not supported in VB.Net, raise warning for manual handling.
         * - Option Explicit: Already set as default in VB.Net, no need to copy.
         * - Option Compare: Defaults to Binary in VB.Net. Only copy if it is set as Text.
         * - Option Private Module: Similar to "Friend" scope, but apply to the entire standard module. Not for class module.
         */

        public override object VisitOptionBaseStmt([NotNull] VisualBasic6Parser.OptionBaseStmtContext context)
        {
            if (context.INTEGERLITERAL() != null)
            {
                if (context.INTEGERLITERAL().GetText() != "0")
                {
                    _rootModule.AddConversionMessage(new UnSupportedNonZeroOptionBase(context.Start.Line, context.Start.Column));
                }
            }
            return null;
        }

        public override object VisitOptionCompareStmt([NotNull] VisualBasic6Parser.OptionCompareStmtContext context)
        {
            if (context.TEXT() != null)
            {
                var cxt = new VB6CodeModelFactoryContext(_rootModule, _rootModule, context, _rootModule.Scope);
                _rootModule.AddCodeModel(new VBOptionCompareStmt(cxt));
            }
            return null;
        }

        public override object VisitOptionPrivateModuleStmt([NotNull] VisualBasic6Parser.OptionPrivateModuleStmtContext context)
        {
            if (_rootModule is VBStandardModule)
                ((VBStandardModule)_rootModule).IsPrivateModule = true;

            return null;
        }

        //public override object VisitOptionExplicitStmt([NotNull] VisualBasic6Parser.OptionExplicitStmtContext context)
        //{
        //    return base.VisitOptionExplicitStmt(context);
        //}

        //public override object VisitModuleOption([NotNull] VisualBasic6Parser.ModuleOptionContext context)
        //{
        //    return base.VisitModuleOption(context);
        //}

        //public override object VisitModuleOptions([NotNull] VisualBasic6Parser.ModuleOptionsContext context)
        //{
        //    return base.VisitModuleOptions(context);
        //}

        #endregion

        public override object VisitModuleBody([NotNull] VisualBasic6Parser.ModuleBodyContext context)
        {
            // This class only extract module level metadata. Not going to visit code blocks.
            return null;
        }

        #endregion

    }
}
