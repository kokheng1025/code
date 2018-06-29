using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB6ActiveXDllConverter.CodeModels
{
    public enum VBProgramScope
    {
        Module,
        Procedure,
        Block,
        MacroBlock // This is used for analyis, not an actual program scope. Variable from different macro block can have the same name, but only one will exist in compiled program.
    }

    public enum VB6Visibility
    {
        Default, // Indicate that the code didn't specify any visibility. Need to use default value.
        Private,
        Public,
        Friend,
        Global // This is old VB keyword, should use Public instead.
    }

    public enum VBDataType
    {
        Boolean,
        Byte,
        Collection,
        Date,
        Double,
        Integer,
        Long,
        Object,
        Single,
        String,
        Variant,
        Currency,
        ComplexType
    }

    public enum ClassInstancingType
    {
        MultiUse,
        GlobalMultiUse,
        PublicNotCreatable,
        Private
    }
}
