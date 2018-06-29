using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    // The topmost base class for all code models.

    /*
     * VBBaseModel 
     *    - VBBaseCommentModel (Normal comment)
     *    - VBBaseCodeModel
     *         - VBBaseModule
     *         - VBBaseStatement
     *              - VBBaseSingleLineStatement (Also indicate start of statement)
     *              - VBBaseSingleLineSubStatement
     *              - VBBaseBlockStatement (Also indicate start of statement)
     *              - VBBaseBlockSubStatement
     *              - VBBaseValueStatement
     *              - VBBaseAtomicCallStatement
     *              - VBBaseAtomicRuleStatement
     * 
     */

    public abstract class VBBaseModel
    {
        internal virtual void Convert(VBNetCodeFileWriter writer) { }
    }
}
