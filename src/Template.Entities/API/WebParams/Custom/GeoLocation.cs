using System;

namespace Template.Entities.API.WebParams.Custom
{
    public class GeoLocation
    {
        private const double EARTH_RADIUS = 6376500;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance => GetLocationDistanceToDelta(Latitude, Longitude, LatitudeDelta, LongitudeDelta);

        public double LatitudeDelta { get; set; }

        public double LongitudeDelta { get; set; }

        private static double GetLocationDistanceToDelta(double latitude, double longitude, double latitudeDelta, double longitudeDelta)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = (latitude + latitudeDelta) * (Math.PI / 180.0);
            var num2 = (longitude + longitudeDelta) * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return EARTH_RADIUS * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
