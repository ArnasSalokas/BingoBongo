using System;

namespace Template.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static long? ToUnixTimeStamp(this DateTime? date) => date.HasValue ? ToUnixTimeStamp(date.Value) : (long?)null;

        public static long ToUnixTimeStamp(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.ToUniversalTime();
            return (int)(date.Subtract(epoch)).TotalSeconds;
        }

        /// <summary>
        /// Returns true if given DateTime is from and to given time.
        /// </summary>
        public static bool IsWithin(this DateTime dateTime, DateTime from, DateTime to, bool inclusive = true) =>
            inclusive
            ? (dateTime >= from && dateTime <= to)
            : (dateTime > from && dateTime < to);

        public static string ToFileStamp(this DateTime dt) => dt.ToString("yyyyMMddHHmm");

        public static string ToDateOnlyStamp(this DateTime dt) => dt.ToString("yyyy-MM-dd");
    }
}
