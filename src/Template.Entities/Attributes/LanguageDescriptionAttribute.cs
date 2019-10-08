using System;

namespace Template.Entities.Attributes
{
    public class LanguageDescriptionAttribute : Attribute
    {
        public string Language { get; }

        public LanguageDescriptionAttribute(string language) => Language = language;
    }
}
