using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Entities.API.Models;
using Template.Entities.API.WebParams.Base;
using Template.Entities.Database.Models;
using Template.Repositories.Base;
using Template.Repositories.Repositories.Contracts;

namespace Template.Repositories.Repositories
{
    public class RequestLogRepository : BaseRepository<RequestLog>, IRequestLogRepository
    {
        #region Filtering constants

        private const string RESPONSE_STATUS_COMPLETED = "Completed";
        private const string RESPONSE_CODE_OK = "Ok";

        #endregion

        public RequestLogRepository(IServiceProvider services) : base(services) { }

        public async Task<RequestLog> GetFirst() =>
            await Store().FirstOrNull<RequestLog>();

        public async Task<ApiStatusTotals> GetRequestCount(DateTime dateFrom, bool failed = false)
        {
            var store = Store();
            var aggregateColumns = new List<AggregateColumn>
            {
                new AggregateColumn(AggregateType.COUNT, nameof(RequestLog.Id), nameof(ApiStatusTotals.Count))
            };

            if (failed)
            {
                store = store.Filtered(nameof(RequestLog.ResponseStatus), RESPONSE_STATUS_COMPLETED, ComparisonOperator.DoesNotEqual, LogicalOperator.OR)
                    .Filtered(nameof(RequestLog.ResponseCode), RESPONSE_CODE_OK, ComparisonOperator.DoesNotEqual, LogicalOperator.OR);
            }

            return await store.Filtered(nameof(RequestLog.RequestSentDate), dateFrom, ComparisonOperator.GreaterThanOrEquals).Aggregate<ApiStatusTotals>(aggregateColumns);
        }

        public async Task<ApiStatusTotals> GetAverageLatency(DateTime dateFrom)
        {
            var aggregateColumns = new List<AggregateColumn>
            {
                new AggregateColumn(AggregateType.AVG, nameof(RequestLog.ResponseElapsedMilliseconds), nameof(ApiStatusTotals.AverageLatency))
            };

            return await Store().Filtered(nameof(RequestLog.RequestSentDate), dateFrom, ComparisonOperator.GreaterThanOrEquals).Aggregate<ApiStatusTotals>(aggregateColumns);
        }
    }
}
