using Dapper;
using Template.Common.Extensions;
using Template.Common.Helpers;
using Template.Entities.API.WebParams.Base;
using Template.Entities.API.WebParams.Custom;

namespace Template.Repositories.Base.Filtering
{
    public class FilteringStDistanceParameter : FilteringParameter
    {
        // parameters names
        #region
        private readonly string _latitudeParam;
        private readonly string _longitudeParam;
        private readonly string _distanceParam;
        #endregion

        public FilteringStDistanceParameter(string propertyName, string parameterName, object value, ComparisonType compareType, ComparisonOperator comparisonOperator, LogicalOperator logicalOperator) : base(
            comparisonOperator, propertyName, parameterName, value, logicalOperator, compareType)
        {
            _latitudeParam = ParameterizeGeoParameters(nameof(GeoLocation.Latitude));
            _longitudeParam = ParameterizeGeoParameters(nameof(GeoLocation.Longitude));
            _distanceParam = ParameterizeGeoParameters(nameof(GeoLocation.Distance));
        }

        public override string ToClause(string tableAlias = null) => $" geography::Point({_latitudeParam}, {_longitudeParam}, 4326).{ComparisonType.GetDescription()}({tableAlias}{ColumnName.Bracketed()}) {GetOperationString()} {_distanceParam}";

        public override void AddToParameters(DynamicParameters parameters)
        {
            var geoLoc = (GeoLocation)Value;

            parameters.Add(_latitudeParam, geoLoc.Latitude);
            parameters.Add(_longitudeParam, geoLoc.Longitude);
            parameters.Add(_distanceParam, geoLoc.Distance);
        }

        private string ParameterizeGeoParameters(string paramName) => $"@{paramName.ToUpper()}{RandomHelper.RandomString(6)}";
    }
}
