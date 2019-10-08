using System;
using System.Collections.Generic;
using System.Text;
using Template.Common.Extensions;

namespace Template.Common.Helpers
{
    public static class ConversionHelper
    {
        public static string FromBase64(string hash) => Encoding.UTF8.GetString(Convert.FromBase64String(hash));

        public static string ToBase64(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        public static T ConvertTo<T>(IConvertible obj) where T : IConvertible => (T)ConvertTo(obj, typeof(T));

        public static object ConvertTo(IConvertible obj, Type convertTo)
        {
            Type t = obj.GetType();

            // T is nullable
            if (convertTo != null)
                return (obj == null) ? convertTo.GetDefaultValue() : Convert.ChangeType(obj, convertTo);

            // T is not nullable
            else
                return Convert.ChangeType(obj, convertTo);
        }

        public static bool InterpretBool(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return false;

            val = val.ToLowerInvariant();

            List<string> booleans = new List<string> { "on", "true", "t", "y", "yes", "1", "enabled" };

            if (booleans.Contains(val))
                return true;

            return false;
        }

        public static bool InterpretFalseBool(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return false;

            val = val.ToLowerInvariant();

            List<string> booleans = new List<string> { "", "0", "false", "f", "n", "no", "off", "disabled", "null" };

            if (booleans.Contains(val))
                return true;

            return false;
        }

        public static decimal ConvertToDecimal(long amount, int decimalPoints) => Convert.ToDecimal(amount / Math.Pow(10, decimalPoints));

        public static long ConvertToLong(decimal amount, int decimalPoints)
        {
            var multiplier = Math.Pow(10, decimalPoints);

            return Convert.ToInt64(Math.Round(amount * (int)multiplier, decimalPoints));
        }
    }
}
