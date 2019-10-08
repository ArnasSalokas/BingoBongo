using System.Text.RegularExpressions;

namespace Template.Common.Extensions
{
    public static class DecimalExtensions
    {
        private const decimal MAX_LONGTITUDE_VALUE = 180M;
        private const decimal MIN_LONGTITUDE_VALUE = -180M;

        private const decimal MAX_LATITUDE_VALUE = 90M;
        private const decimal MIN_LATITUDE_VALUE = -90M;

        private static readonly Regex COORDINATE_DECIMAL_DEGREE = new Regex(@"[-]?\d{1,3}([\.|\,]\d{1,8})");

        public static bool IsValidLatitude(this decimal latitude) => IsValidDecimalDegree(latitude) && latitude <= MAX_LATITUDE_VALUE && latitude >= MIN_LATITUDE_VALUE;

        public static bool IsValidLongitude(this decimal longtitude) => IsValidDecimalDegree(longtitude) && longtitude <= MAX_LONGTITUDE_VALUE && longtitude >= MIN_LONGTITUDE_VALUE;

        public static bool IsValidDecimalDegree(this decimal coordinate) => COORDINATE_DECIMAL_DEGREE.IsMatch(coordinate.ToString());
    }
}
