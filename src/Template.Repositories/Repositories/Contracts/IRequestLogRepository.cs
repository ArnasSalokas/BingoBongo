using System;
using System.Threading.Tasks;
using Template.Entities.API.Models;
using Template.Entities.Database.Models;
using Template.Repositories.Base.Contracts;

namespace Template.Repositories.Repositories.Contracts
{
    public interface IRequestLogRepository : IBaseRepository<RequestLog>
    {
        /// <summary>
        /// Gets the top request log from database, used to test connectivity to database
        /// </summary>
        /// <returns></returns>
        Task<RequestLog> GetFirst();

        /// <summary>
        /// Gets request count from a given date till now, if failed is set to true returns failed request count
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        Task<ApiStatusTotals> GetRequestCount(DateTime dateFrom, bool failed = false);

        /// <summary>
        /// Gets the average latency in a given time amount
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <returns></returns>
        Task<ApiStatusTotals> GetAverageLatency(DateTime dateFrom);
    }
}
