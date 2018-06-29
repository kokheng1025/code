using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr4.Runtime;
using VB6ActiveXDllConverter.Parsers.VB6;
using VB6ActiveXDllConverter.ConversionMessages;
using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverter.CodeModels
{
    public class VBBaseType : VBBaseAtomicRuleStatement
    {
        /*
         * baseType
         * : BOOLEAN
         * | BYTE
         * | COLLECTION
         * | DATE
         * | DOUBLE
         * | INTEGER
         * | LONG
         * | OBJECT
         * | SINGLE
         * | STRING
         * | VARIANT
         * 
         */

        internal VBBaseType(VB6CodeModelFactoryContext factoryContext) : base(factoryContext) { }

        // For Demo
        internal override void Convert(VBNetCodeFileWriter writer)
        {
            var ctx = ParserContext as VisualBasic6Parser.BaseTypeContext;
            if (ctx.VARIANT() != null)
            {
                writer.Write("Object");
            }
            else
            {
                base.Convert(writer);
            }
        }
    }
}
