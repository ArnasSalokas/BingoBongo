using System;

namespace Template.Entities.Attributes
{
    public class PaymentSettingDescriptionAttribute : Attribute
    {
        public string Type { get; set; }

        public PaymentSettingDescriptionAttribute(string type) => Type = type;
    }
}
