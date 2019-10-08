using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Template.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum element)
        {
            FieldInfo fi = element.GetType().GetField(element.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes?.Length > 0)
                return attributes[0].Description;

            else
                return element.ToString();
        }

        public static IEnumerable<Enum> GetFlags(this Enum element)
        {
            if (element != null)
                return Enum.GetValues(element.GetType()).Cast<Enum>().Where(element.HasFlag);

            return null;
        }

        public static string ToStringInt(this Enum element) => (Convert.ToInt32(element)).ToString();

        public static AttributeType GetAttribute<AttributeType>(this Enum element)
        {
            var enumType = element.GetType();
            var memInfo = enumType.GetMember(element.ToString());

            if (memInfo.Length > 0)
                return (AttributeType)memInfo[0].GetCustomAttributes(typeof(AttributeType), false)[0];

            return default(AttributeType);
        }
    }
}
