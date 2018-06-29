using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBScope
    {
        public VBScope ParentScope { get; }
        public VBBaseCodeModel CodeModel { get; }
        public VBProgramScope ScopeLevel { get; }

        private List<VBScope> _childs = new List<VBScope>();
        public ReadOnlyCollection<VBScope> ChildScopes { get; }

        internal VBScope(VBScope parentScope, VBBaseCodeModel codeModel, VBProgramScope level)
        {
            ParentScope = parentScope;
            CodeModel = codeModel;
            ScopeLevel = level;
            ChildScopes = new ReadOnlyCollection<VBScope>(_childs);

            if (parentScope != null)
                parentScope.AddChildScope(this);
        }

        internal void AddChildScope(VBScope childScope)
        {
            _childs.Add(childScope);
        }
    }
}
