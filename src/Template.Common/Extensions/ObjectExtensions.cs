using System;

namespace Template.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsDefaultValue<T>(this T value) => value.IsDefaultValue(typeof(T));

        public static bool IsDefaultValue(this object value) => value == null || value.IsDefaultValue(value.GetType());

        public static bool IsDefaultValue(this object value, Type type)
        {
            if (type == null)
                return false;

            if (type.IsValueType)
                return value != null ? value.Equals(type.GetDefaultValue()) : type.IsNullable();

            return value == null;
        }

        public static object GetDefaultValue(this Type t) => t.IsValueType ? Activator.CreateInstance(t) : null;

        public static bool IsNullable(this Type type) => Nullable.GetUnderlyingType(type) != null;
    }
}
