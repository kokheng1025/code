using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;
using VB6ActiveXDllConverter.Converters;
using VB6ActiveXDllConverter.Visitors;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBSubStmt : VBBaseBlockStatement
    {
        /*
         * subStmt
         * : (visibility WS)? (STATIC WS)? SUB WS ambiguousIdentifier (WS? argList)? NEWLINE + (block NEWLINE +)? END_SUB
         * 
         */

        private bool _isConstructor = false;

        internal VBSubStmt(VB6CodeModelFactoryContext factoryContext) : base(factoryContext)
        {
            Scope = new VBScope(factoryContext.Scope, this, VBProgramScope.Procedure);
        }


        // For Demo
        internal override void BeforeVisitChild()
        {
            // Let base class parse other things first before adding warning
            base.BeforeVisitChild();

            // Give warning for Class_Terminate, cannot convert with 100% same behavior, requires manual re-factoring if doing critical tasks.
            var ctx = ParserContext as VisualBasic6Parser.SubStmtContext;
            var methodName = ctx.ambiguousIdentifier().GetText();
            if (methodName.Equals("Class_Terminate", StringComparison.InvariantCultureIgnoreCase))
            {
                var m = new UnSupportedClassTerminate(ParserContext.Start.Line, ParserContext.Start.Column);
                RootModule.AddConversionMessage(m);
                var c = new VBConversionMessage(m);
                ParentCodeBlock.AddCodeModel(c);
            }
        }

        // For Demo
        internal override void VisitChild()
        {
            var ctx = ParserContext as VisualBasic6Parser.SubStmtContext;
            var methodName = ctx.ambiguousIdentifier().GetText();
            _isConstructor = (methodName.Equals("Class_Initialize", StringComparison.InvariantCultureIgnoreCase));

            base.VisitChild();
        }

        // For Demo
        internal override void Convert(VBNetCodeFileWriter writer)
        {
            if (_isConstructor)
            {
                foreach (var model in CodeModels)
                {
                    switch (model)
                    {
                        case VBVisibility v:
                            var r = RootModule as VBClassModule;
                            if ((r.InstancingType == ClassInstancingType.Private) || (r.InstancingType == ClassInstancingType.PublicNotCreatable))
                                writer.Write("Private");
                            else
                                writer.Write("Public");
                            break;

                        case VBAmbiguousIdentifier a:
                            writer.Write("New"); // Replace "Class_Initialize" with VB.net "New" constructor
                            break;

                        default:
                            model.Convert(writer);
                            break;
                    }
                }
            }
            else
            {
                base.Convert(writer);
            }
        }
    }
}
