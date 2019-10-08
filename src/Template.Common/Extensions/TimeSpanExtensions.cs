using System;

namespace Template.Common.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns true if given TimeSpan is from and to given time.
        /// </summary>
        public static bool IsWithin(this TimeSpan timeSpan, TimeSpan from, TimeSpan to, bool inclusive = true) =>
            inclusive
            ? (timeSpan >= from && timeSpan <= to)
            : (timeSpan > from && timeSpan < to);

        /// <summary>
        /// Multiplies a timespan by an integer value
        /// </summary>
        public static TimeSpan MultiplyByInt(this TimeSpan multiplicand, int multiplier)
        {
            var multipliedResult = TimeSpan.FromTicks((long)(multiplicand.Ticks * multiplier));

            /// Making sure that multiplied result doesn't exceed the max value of TimeSpan.
            if (multipliedResult < TimeSpan.MaxValue)
                return multipliedResult;

            return TimeSpan.MaxValue;
        }
    }
}
