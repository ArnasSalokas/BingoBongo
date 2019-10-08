using System;

namespace Template.Common.Attributes
{
    public class Country2ISOCodeAttribute : Attribute
    {
        public string Value { get; }

        public Country2ISOCodeAttribute(string val) => Value = val;
    }
}
