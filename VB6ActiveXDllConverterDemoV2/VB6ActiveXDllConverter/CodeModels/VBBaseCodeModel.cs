using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    // Represent code related tokens
    public abstract class VBBaseCodeModel : VBBaseModel
    {
        protected List<VBBaseModel> _codeModels = new List<VBBaseModel>();
        internal ReadOnlyCollection<VBBaseModel> CodeModels { get; }
        internal ParserRuleContext ParserContext { get; } //{ get { return _context; } }

        public VBScope Scope { get; internal set; }

        internal VBBaseCodeModel(ParserRuleContext context)
        {
            ParserContext = context;
            CodeModels = new ReadOnlyCollection<VBBaseModel>(_codeModels);
        }

        public void AddCodeModel(VBBaseModel model)
        {
            _codeModels.Add(model);
        }

        internal virtual void BeforeVisitChild() { }

        internal virtual void VisitChild() { }

        internal virtual void AfterVisitChild() { }
    }
}
