using System;

namespace Template.Common.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToStringWithLeadingZeroes(this int integer, int leadingZeroes) => integer.ToString($"D{leadingZeroes}");

        public static DateTime? FromUnixTimeStamp(this long? timeStamp)
        {
            if (timeStamp == null)
                return null;

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(timeStamp.Value);
        }

        public static DateTime FromUnixTimeStamp(this long timeStamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(timeStamp);
        }
    }
}
