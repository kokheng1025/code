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
    internal class VB6CodeModelVisitor : VisualBasic6BaseVisitor<object>
    {
        private VisualBasic6Lexer _vb6Lexer;
        private VisualBasic6Parser _vb6Parser;
        private CommonTokenStream _commonTokenStream;
        private VB6CodeModelFactory _codeModelFactory;
        private VBBaseModule _rootModule; // Always point to the root module. (Standard, Class module & etc)
        private VBBaseCodeModel _parentCodeBlock; // Immediate parent of the current code. Can be Sub/Function, If..Then..Else or other block statement.

        public VBBaseModel FirstModel = null; // The first model created by this visitor.

        internal VB6CodeModelVisitor(VBBaseModule rootModule, VBBaseCodeModel parentCodeBlock, VisualBasic6Lexer vb6Lexer, VisualBasic6Parser vb6Parser, CommonTokenStream commonTokenStream)
        {
            _rootModule = rootModule;
            _vb6Lexer = vb6Lexer;
            _vb6Parser = vb6Parser;
            _commonTokenStream = commonTokenStream;
            _parentCodeBlock = parentCodeBlock;

            _codeModelFactory = new VB6CodeModelFactory(_rootModule, _parentCodeBlock);
        }

        public void PostProcess()
        {
            // Extract comments at the end of file if any.
            if (_rootModule.LastScanTokenIndex > _parentCodeBlock.ParserContext.Stop.TokenIndex)
                return; // Already scanned in child node.

            for (int i = _rootModule.LastScanTokenIndex; i <= _parentCodeBlock.ParserContext.Stop.TokenIndex; i++)
            {
                var token = _commonTokenStream.Get(i);
                switch (token.Type)
                {
                    case VisualBasic6Parser.NEWLINE:
                        _parentCodeBlock.AddCodeModel(new VBNewLine(token));
                        break;
                    case VisualBasic6Parser.COMMENT:
                        var isOnNewLine = true;
                        if (token.TokenIndex > 0)
                            isOnNewLine = (_commonTokenStream.Get(token.TokenIndex - 1).Type == VisualBasic6Parser.NEWLINE);
                        _parentCodeBlock.AddCodeModel(new VBComment(token, isOnNewLine));
                        break;
                    default:
                        // ignore the rest
                        break;
                }
            }
            _rootModule.LastScanTokenIndex = _parentCodeBlock.ParserContext.Stop.TokenIndex + 1;
        }

        #region Visitor methods

        // Generic entry point whre the visit process starts
        public override object Visit(IParseTree tree)
        {
            // Quit, just in case if module body do not have any code (Empty module file)
            if (tree == null)
                return null;
            else
                return base.Visit(tree);
        }

        private object CreateModel(ParserRuleContext context)
        {
            var cModel = _codeModelFactory.CreateCodeModel(context);
            if (FirstModel == null)
                FirstModel = cModel;

            return cModel; // _codeModelFactory.CreateCodeModel(context);
        }

        private object CreateModel(ITerminalNode node)
        {
            var cModel = _codeModelFactory.CreateCodeModel(node);
            if (FirstModel == null)
                FirstModel = cModel;

            return cModel; // _codeModelFactory.CreateCodeModel(node);
        }


        #region Module Body Elements

        public override object VisitDeclareStmt([NotNull] VisualBasic6Parser.DeclareStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitEnumerationStmt([NotNull] VisualBasic6Parser.EnumerationStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitEnumerationStmt_Constant([NotNull] VisualBasic6Parser.EnumerationStmt_ConstantContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitEventStmt([NotNull] VisualBasic6Parser.EventStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitFunctionStmt([NotNull] VisualBasic6Parser.FunctionStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitMacroConstStmt([NotNull] VisualBasic6Parser.MacroConstStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPropertyGetStmt([NotNull] VisualBasic6Parser.PropertyGetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPropertySetStmt([NotNull] VisualBasic6Parser.PropertySetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPropertyLetStmt([NotNull] VisualBasic6Parser.PropertyLetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSubStmt([NotNull] VisualBasic6Parser.SubStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitTypeStmt([NotNull] VisualBasic6Parser.TypeStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitTypeStmt_Element([NotNull] VisualBasic6Parser.TypeStmt_ElementContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Module Body Elements: Macro If Then Else

        public override object VisitMacroIfThenElseStmt([NotNull] VisualBasic6Parser.MacroIfThenElseStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitMacroIfBlockStmt([NotNull] VisualBasic6Parser.MacroIfBlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitMacroElseIfBlockStmt([NotNull] VisualBasic6Parser.MacroElseIfBlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitMacroElseBlockStmt([NotNull] VisualBasic6Parser.MacroElseBlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Block statements

        public override object VisitAppActivateStmt([NotNull] VisualBasic6Parser.AppActivateStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitBeepStmt([NotNull] VisualBasic6Parser.BeepStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitChDirStmt([NotNull] VisualBasic6Parser.ChDirStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitChDriveStmt([NotNull] VisualBasic6Parser.ChDriveStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitCloseStmt([NotNull] VisualBasic6Parser.CloseStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitConstStmt([NotNull] VisualBasic6Parser.ConstStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitConstSubStmt([NotNull] VisualBasic6Parser.ConstSubStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitDateStmt([NotNull] VisualBasic6Parser.DateStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitDeleteSettingStmt([NotNull] VisualBasic6Parser.DeleteSettingStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitDeftypeStmt([NotNull] VisualBasic6Parser.DeftypeStmtContext context)
        {
            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa263421(v=vs.60)
            CreateModel(context);
            return null;
        }

        public override object VisitDoLoopStmt([NotNull] VisualBasic6Parser.DoLoopStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitEndStmt([NotNull] VisualBasic6Parser.EndStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitEraseStmt([NotNull] VisualBasic6Parser.EraseStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitErrorStmt([NotNull] VisualBasic6Parser.ErrorStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitExitStmt([NotNull] VisualBasic6Parser.ExitStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitFilecopyStmt([NotNull] VisualBasic6Parser.FilecopyStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitForEachStmt([NotNull] VisualBasic6Parser.ForEachStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitForNextStmt([NotNull] VisualBasic6Parser.ForNextStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitGetStmt([NotNull] VisualBasic6Parser.GetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitGoSubStmt([NotNull] VisualBasic6Parser.GoSubStmtContext context)
        {
            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa243378(v=vs.60)
            CreateModel(context);
            return null;
        }

        public override object VisitReturnStmt([NotNull] VisualBasic6Parser.ReturnStmtContext context)
        {
            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa243378(v=vs.60)
            CreateModel(context);
            return null;
        }

        public override object VisitGoToStmt([NotNull] VisualBasic6Parser.GoToStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitImplementsStmt([NotNull] VisualBasic6Parser.ImplementsStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitInputStmt([NotNull] VisualBasic6Parser.InputStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitKillStmt([NotNull] VisualBasic6Parser.KillStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLetStmt([NotNull] VisualBasic6Parser.LetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLineInputStmt([NotNull] VisualBasic6Parser.LineInputStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLineLabel([NotNull] VisualBasic6Parser.LineLabelContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLoadStmt([NotNull] VisualBasic6Parser.LoadStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLockStmt([NotNull] VisualBasic6Parser.LockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLsetStmt([NotNull] VisualBasic6Parser.LsetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitMidStmt([NotNull] VisualBasic6Parser.MidStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitMkdirStmt([NotNull] VisualBasic6Parser.MkdirStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitNameStmt([NotNull] VisualBasic6Parser.NameStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitOnErrorStmt([NotNull] VisualBasic6Parser.OnErrorStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitOnGoToStmt([NotNull] VisualBasic6Parser.OnGoToStmtContext context)
        {
            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa266175(v=vs.60)
            CreateModel(context);
            return null;
        }

        public override object VisitOnGoSubStmt([NotNull] VisualBasic6Parser.OnGoSubStmtContext context)
        {
            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa266175(v=vs.60)
            CreateModel(context);
            return null;
        }

        public override object VisitOpenStmt([NotNull] VisualBasic6Parser.OpenStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitOutputList([NotNull] VisualBasic6Parser.OutputListContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitOutputList_Expression([NotNull] VisualBasic6Parser.OutputList_ExpressionContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPrintStmt([NotNull] VisualBasic6Parser.PrintStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPutStmt([NotNull] VisualBasic6Parser.PutStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitRaiseEventStmt([NotNull] VisualBasic6Parser.RaiseEventStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitRandomizeStmt([NotNull] VisualBasic6Parser.RandomizeStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitRedimStmt([NotNull] VisualBasic6Parser.RedimStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitRedimSubStmt([NotNull] VisualBasic6Parser.RedimSubStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitResetStmt([NotNull] VisualBasic6Parser.ResetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitResumeStmt([NotNull] VisualBasic6Parser.ResumeStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitRmdirStmt([NotNull] VisualBasic6Parser.RmdirStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitRsetStmt([NotNull] VisualBasic6Parser.RsetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSavepictureStmt([NotNull] VisualBasic6Parser.SavepictureStmtContext context)
        {
            // Not supported in VB.Net
            // VB6 reference: https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-basic-6/aa445827(v=vs.60)
            CreateModel(context);
            return null;
        }

        public override object VisitSaveSettingStmt([NotNull] VisualBasic6Parser.SaveSettingStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSeekStmt([NotNull] VisualBasic6Parser.SeekStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSendkeysStmt([NotNull] VisualBasic6Parser.SendkeysStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSetattrStmt([NotNull] VisualBasic6Parser.SetattrStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSetStmt([NotNull] VisualBasic6Parser.SetStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitStopStmt([NotNull] VisualBasic6Parser.StopStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitTimeStmt([NotNull] VisualBasic6Parser.TimeStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitUnloadStmt([NotNull] VisualBasic6Parser.UnloadStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitUnlockStmt([NotNull] VisualBasic6Parser.UnlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVariableStmt([NotNull] VisualBasic6Parser.VariableStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVariableListStmt([NotNull] VisualBasic6Parser.VariableListStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVariableSubStmt([NotNull] VisualBasic6Parser.VariableSubStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitWhileWendStmt([NotNull] VisualBasic6Parser.WhileWendStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitWidthStmt([NotNull] VisualBasic6Parser.WidthStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitWithStmt([NotNull] VisualBasic6Parser.WithStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitWriteStmt([NotNull] VisualBasic6Parser.WriteStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitTypeOfStmt([NotNull] VisualBasic6Parser.TypeOfStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Block statements: If Then Else

        // This one seems like not hit, handle its child rule instead
        public override object VisitIfThenElseStmt([NotNull] VisualBasic6Parser.IfThenElseStmtContext context)
        {
            return base.VisitIfThenElseStmt(context);
        }

        public override object VisitInlineIfThenElse([NotNull] VisualBasic6Parser.InlineIfThenElseContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitBlockIfThenElse([NotNull] VisualBasic6Parser.BlockIfThenElseContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitIfBlockStmt([NotNull] VisualBasic6Parser.IfBlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitIfConditionStmt([NotNull] VisualBasic6Parser.IfConditionStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitIfElseIfBlockStmt([NotNull] VisualBasic6Parser.IfElseIfBlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitIfElseBlockStmt([NotNull] VisualBasic6Parser.IfElseBlockStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Block statements: Select Case

        public override object VisitSelectCaseStmt([NotNull] VisualBasic6Parser.SelectCaseStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSC_Case([NotNull] VisualBasic6Parser.SC_CaseContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSC_Cond([NotNull] VisualBasic6Parser.SC_CondContext context)
        {
            // Not sure if this is hit
            return base.VisitSC_Cond(context);
        }

        public override object VisitSC_CondExpr([NotNull] VisualBasic6Parser.SC_CondExprContext context)
        {
            // Not sure if this is hit
            // This is the condition expression inside a case
            return base.VisitSC_CondExpr(context);
        }

        public override object VisitCaseCondElse([NotNull] VisualBasic6Parser.CaseCondElseContext context)
        {
            // This is Case Else part
            CreateModel(context);
            return null;
        }

        public override object VisitCaseCondExpr([NotNull] VisualBasic6Parser.CaseCondExprContext context)
        {
            // This is the normal case branch with condition
            CreateModel(context);
            return null;
        }

        public override object VisitCaseCondExprIs([NotNull] VisualBasic6Parser.CaseCondExprIsContext context)
        {
            // This is the condition expression inside a case: Using Is operator
            CreateModel(context);
            return null;
        }

        public override object VisitCaseCondExprTo([NotNull] VisualBasic6Parser.CaseCondExprToContext context)
        {
            // This is the condition expression inside a case: Using To operator
            CreateModel(context);
            return null;
        }

        public override object VisitCaseCondExprValue([NotNull] VisualBasic6Parser.CaseCondExprValueContext context)
        {
            // This is the condition expression inside a case: Using normal expression other than Is/To
            CreateModel(context);
            return null;
        }

        #endregion

        #region Value statements

        // Placeholder, need to handle its child rules instead.
        public override object VisitValueStmt([NotNull] VisualBasic6Parser.ValueStmtContext context)
        {
            // Seems like never hit
            return base.VisitValueStmt(context);
        }

        public override object VisitVsLiteral([NotNull] VisualBasic6Parser.VsLiteralContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsStruct([NotNull] VisualBasic6Parser.VsStructContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsNew([NotNull] VisualBasic6Parser.VsNewContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsTypeOf([NotNull] VisualBasic6Parser.VsTypeOfContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsAddressOf([NotNull] VisualBasic6Parser.VsAddressOfContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsAssign([NotNull] VisualBasic6Parser.VsAssignContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsPow([NotNull] VisualBasic6Parser.VsPowContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsNegation([NotNull] VisualBasic6Parser.VsNegationContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsPlus([NotNull] VisualBasic6Parser.VsPlusContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsDiv([NotNull] VisualBasic6Parser.VsDivContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsMult([NotNull] VisualBasic6Parser.VsMultContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsMod([NotNull] VisualBasic6Parser.VsModContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsAdd([NotNull] VisualBasic6Parser.VsAddContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsMinus([NotNull] VisualBasic6Parser.VsMinusContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsAmp([NotNull] VisualBasic6Parser.VsAmpContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsEq([NotNull] VisualBasic6Parser.VsEqContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsNeq([NotNull] VisualBasic6Parser.VsNeqContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsLt([NotNull] VisualBasic6Parser.VsLtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsGt([NotNull] VisualBasic6Parser.VsGtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsLeq([NotNull] VisualBasic6Parser.VsLeqContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsGeq([NotNull] VisualBasic6Parser.VsGeqContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsLike([NotNull] VisualBasic6Parser.VsLikeContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsIs([NotNull] VisualBasic6Parser.VsIsContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsNot([NotNull] VisualBasic6Parser.VsNotContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsAnd([NotNull] VisualBasic6Parser.VsAndContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsOr([NotNull] VisualBasic6Parser.VsOrContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsXor([NotNull] VisualBasic6Parser.VsXorContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsEqv([NotNull] VisualBasic6Parser.VsEqvContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsImp([NotNull] VisualBasic6Parser.VsImpContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsICS([NotNull] VisualBasic6Parser.VsICSContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVsMid([NotNull] VisualBasic6Parser.VsMidContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Complex Call statements

        // Placeholder, need to handle its child rules instead.
        public override object VisitExplicitCallStmt([NotNull] VisualBasic6Parser.ExplicitCallStmtContext context)
        {
            return base.VisitExplicitCallStmt(context);
        }

        public override object VisitECS_ProcedureCall([NotNull] VisualBasic6Parser.ECS_ProcedureCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitECS_MemberProcedureCall([NotNull] VisualBasic6Parser.ECS_MemberProcedureCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitImplicitCallStmt_InBlock([NotNull] VisualBasic6Parser.ImplicitCallStmt_InBlockContext context)
        {
            // Placeholder, need to handle its child rules instead.
            return base.VisitImplicitCallStmt_InBlock(context);
        }

        public override object VisitICS_B_ProcedureCall([NotNull] VisualBasic6Parser.ICS_B_ProcedureCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitICS_B_MemberProcedureCall([NotNull] VisualBasic6Parser.ICS_B_MemberProcedureCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitImplicitCallStmt_InStmt([NotNull] VisualBasic6Parser.ImplicitCallStmt_InStmtContext context)
        {
            // Placeholder, need to handle its child rules instead.
            return base.VisitImplicitCallStmt_InStmt(context);
        }

        public override object VisitICS_S_MembersCall([NotNull] VisualBasic6Parser.ICS_S_MembersCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitICS_S_MemberCall([NotNull] VisualBasic6Parser.ICS_S_MemberCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitICS_S_VariableOrProcedureCall([NotNull] VisualBasic6Parser.ICS_S_VariableOrProcedureCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitICS_S_ProcedureOrArrayCall([NotNull] VisualBasic6Parser.ICS_S_ProcedureOrArrayCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitICS_S_DictionaryCall([NotNull] VisualBasic6Parser.ICS_S_DictionaryCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitICS_S_NestedProcedureCall([NotNull] VisualBasic6Parser.ICS_S_NestedProcedureCallContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Atomic Call statements

        public override object VisitArgsCall([NotNull] VisualBasic6Parser.ArgsCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitArgCall([NotNull] VisualBasic6Parser.ArgCallContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitDictionaryCallStmt([NotNull] VisualBasic6Parser.DictionaryCallStmtContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitArgList([NotNull] VisualBasic6Parser.ArgListContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitArg([NotNull] VisualBasic6Parser.ArgContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitArgDefaultValue([NotNull] VisualBasic6Parser.ArgDefaultValueContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSubscripts([NotNull] VisualBasic6Parser.SubscriptsContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitSubscript([NotNull] VisualBasic6Parser.SubscriptContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Atomic rules

        public override object VisitAmbiguousIdentifier([NotNull] VisualBasic6Parser.AmbiguousIdentifierContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitAsTypeClause([NotNull] VisualBasic6Parser.AsTypeClauseContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitBaseType([NotNull] VisualBasic6Parser.BaseTypeContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitCertainIdentifier([NotNull] VisualBasic6Parser.CertainIdentifierContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitComparisonOperator([NotNull] VisualBasic6Parser.ComparisonOperatorContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitComplexType([NotNull] VisualBasic6Parser.ComplexTypeContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitFieldLength([NotNull] VisualBasic6Parser.FieldLengthContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLetterrange([NotNull] VisualBasic6Parser.LetterrangeContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitLiteral([NotNull] VisualBasic6Parser.LiteralContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPublicPrivateVisibility([NotNull] VisualBasic6Parser.PublicPrivateVisibilityContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitPublicPrivateGlobalVisibility([NotNull] VisualBasic6Parser.PublicPrivateGlobalVisibilityContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitType([NotNull] VisualBasic6Parser.TypeContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitTypeHint([NotNull] VisualBasic6Parser.TypeHintContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitVisibility([NotNull] VisualBasic6Parser.VisibilityContext context)
        {
            CreateModel(context);
            return null;
        }

        public override object VisitAmbiguousKeyword([NotNull] VisualBasic6Parser.AmbiguousKeywordContext context)
        {
            CreateModel(context);
            return null;
        }

        #endregion

        #region Terminal nodes

        public override object VisitTerminal(ITerminalNode node)
        {
            var nodeType = node.Symbol.Type;
            switch (nodeType)
            {
                case VisualBasic6Parser.LINE_CONTINUATION:
                    break;
                case VisualBasic6Parser.NEWLINE:
                    break;
                case VisualBasic6Parser.COMMENT:
                    break;
                //case VisualBasic6Parser.TAB:
                //    break;
                //case VisualBasic6Parser.WS:
                //    CreateModel(node);
                //    break;
                default:
                    CreateModel(node);
                    break;
            }

            return base.VisitTerminal(node);
        }

        #endregion

        #region Not used for now

        #region Generic ANTLR4 methods

        protected override object DefaultResult => base.DefaultResult;

        protected override object AggregateResult(object aggregate, object nextResult)
        {
            return base.AggregateResult(aggregate, nextResult);
        }

        public override object VisitStartRule([NotNull] VisualBasic6Parser.StartRuleContext context)
        {
            return base.VisitStartRule(context);
        }

        protected override bool ShouldVisitNextChild(IRuleNode node, object currentResult)
        {
            return base.ShouldVisitNextChild(node, currentResult);
        }

        public override object VisitChildren(IRuleNode node)
        {
            return base.VisitChildren(node);
        }

        public override object VisitErrorNode(IErrorNode node)
        {
            return base.VisitErrorNode(node);
        }

        #endregion

        #region Module related rules, already handled in module visitor

        public override object VisitModule([NotNull] VisualBasic6Parser.ModuleContext context)
        {
            return base.VisitModule(context);
        }
        public override object VisitModuleHeader([NotNull] VisualBasic6Parser.ModuleHeaderContext context)
        {
            return base.VisitModuleHeader(context);
        }

        public override object VisitModuleReferences([NotNull] VisualBasic6Parser.ModuleReferencesContext context)
        {
            return base.VisitModuleReferences(context);
        }

        public override object VisitModuleReference([NotNull] VisualBasic6Parser.ModuleReferenceContext context)
        {
            return base.VisitModuleReference(context);
        }

        public override object VisitModuleReferenceComponent([NotNull] VisualBasic6Parser.ModuleReferenceComponentContext context)
        {
            return base.VisitModuleReferenceComponent(context);
        }

        public override object VisitModuleReferenceValue([NotNull] VisualBasic6Parser.ModuleReferenceValueContext context)
        {
            return base.VisitModuleReferenceValue(context);
        }

        public override object VisitModuleConfig([NotNull] VisualBasic6Parser.ModuleConfigContext context)
        {
            return base.VisitModuleConfig(context);
        }

        public override object VisitModuleConfigElement([NotNull] VisualBasic6Parser.ModuleConfigElementContext context)
        {
            return base.VisitModuleConfigElement(context);
        }

        public override object VisitModuleAttributes([NotNull] VisualBasic6Parser.ModuleAttributesContext context)
        {
            return base.VisitModuleAttributes(context);
        }

        public override object VisitAttributeStmt([NotNull] VisualBasic6Parser.AttributeStmtContext context)
        {
            return base.VisitAttributeStmt(context);
        }

        public override object VisitModuleOptions([NotNull] VisualBasic6Parser.ModuleOptionsContext context)
        {
            return base.VisitModuleOptions(context);
        }

        public override object VisitModuleOption([NotNull] VisualBasic6Parser.ModuleOptionContext context)
        {
            return base.VisitModuleOption(context);
        }

        public override object VisitOptionBaseStmt([NotNull] VisualBasic6Parser.OptionBaseStmtContext context)
        {
            return base.VisitOptionBaseStmt(context);
        }

        public override object VisitOptionCompareStmt([NotNull] VisualBasic6Parser.OptionCompareStmtContext context)
        {
            return base.VisitOptionCompareStmt(context);
        }

        public override object VisitOptionExplicitStmt([NotNull] VisualBasic6Parser.OptionExplicitStmtContext context)
        {
            return base.VisitOptionExplicitStmt(context);
        }

        public override object VisitOptionPrivateModuleStmt([NotNull] VisualBasic6Parser.OptionPrivateModuleStmtContext context)
        {
            return base.VisitOptionPrivateModuleStmt(context);
        }

        public override object VisitModuleBody([NotNull] VisualBasic6Parser.ModuleBodyContext context)
        {
            return base.VisitModuleBody(context);
        }

        public override object VisitModuleBodyElement([NotNull] VisualBasic6Parser.ModuleBodyElementContext context)
        {
            return base.VisitModuleBodyElement(context);
        }

        public override object VisitModuleBlock([NotNull] VisualBasic6Parser.ModuleBlockContext context)
        {
            return base.VisitModuleBlock(context);
        }

        #endregion

        #region Empty placeholder rules, should handle the concrete child rules instead

        public override object VisitBlock([NotNull] VisualBasic6Parser.BlockContext context)
        {
            return base.VisitBlock(context);
        }

        public override object VisitBlockStmt([NotNull] VisualBasic6Parser.BlockStmtContext context)
        {
            return base.VisitBlockStmt(context);
        }

        #endregion

        #region UI controls related. Not used for now.

        public override object VisitControlProperties([NotNull] VisualBasic6Parser.ControlPropertiesContext context)
        {
            return base.VisitControlProperties(context);
        }

        public override object VisitCp_Properties([NotNull] VisualBasic6Parser.Cp_PropertiesContext context)
        {
            return base.VisitCp_Properties(context);
        }

        public override object VisitCp_SingleProperty([NotNull] VisualBasic6Parser.Cp_SinglePropertyContext context)
        {
            return base.VisitCp_SingleProperty(context);
        }

        public override object VisitCp_PropertyName([NotNull] VisualBasic6Parser.Cp_PropertyNameContext context)
        {
            return base.VisitCp_PropertyName(context);
        }

        public override object VisitCp_PropertyValue([NotNull] VisualBasic6Parser.Cp_PropertyValueContext context)
        {
            return base.VisitCp_PropertyValue(context);
        }

        public override object VisitCp_NestedProperty([NotNull] VisualBasic6Parser.Cp_NestedPropertyContext context)
        {
            return base.VisitCp_NestedProperty(context);
        }

        public override object VisitCp_ControlType([NotNull] VisualBasic6Parser.Cp_ControlTypeContext context)
        {
            return base.VisitCp_ControlType(context);
        }

        public override object VisitCp_ControlIdentifier([NotNull] VisualBasic6Parser.Cp_ControlIdentifierContext context)
        {
            return base.VisitCp_ControlIdentifier(context);
        }

        #endregion

        #endregion

        #endregion
    }
}
