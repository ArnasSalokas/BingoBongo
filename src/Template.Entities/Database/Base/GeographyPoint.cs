namespace Template.Entities.Database.Base
{
    /// <summary>
    /// Marked class used to initialize spatial data type in sql
    /// </summary>
    public class GeographyPoint : IGeographyPoint
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public GeographyPoint(decimal lat, decimal lng)
        {
            Latitude = lat;
            Longitude = lng;
        }

        public GeographyPoint() { }
    }
}
