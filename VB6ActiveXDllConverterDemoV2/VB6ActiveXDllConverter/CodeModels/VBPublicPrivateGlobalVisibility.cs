using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBPublicPrivateGlobalVisibility : VBBaseAtomicRuleStatement
    {
        /*
         * publicPrivateGlobalVisibility
         * : PRIVATE
         * | PUBLIC
         * | GLOBAL
         * 
         */

        internal VBPublicPrivateGlobalVisibility(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }
    }
}
