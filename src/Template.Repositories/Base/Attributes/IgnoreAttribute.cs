using System;

namespace Template.Repositories.Base.Attributes
{
    public class IgnoreAttribute : Attribute
    {
        public IgnoreTypeEnum Type { get; }

        public IgnoreAttribute() => Type = IgnoreTypeEnum.All;

        public IgnoreAttribute(IgnoreTypeEnum ignoreType) => Type = ignoreType;
    }

    [Flags]
    public enum IgnoreTypeEnum
    {
        Select = 1,
        Insert = 2,
        Update = 4,
        Writes = 6,
        All = 7
    }
}
