using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using Template.Entities.Database.Models;
using Template.Repositories.Repositories.Contracts;

namespace Template.Services.Base
{
    public abstract class BaseRestService : BaseService
    {
        private readonly IRequestLogRepository _requestLogRepository;

        public string BaseUrl { get; set; }
        
        public string ApiKey { get; set; }

        public BaseRestService(IServiceProvider services) : base(services)
        {
            _requestLogRepository = _services.GetRequestLogRepository();
        }

        public RestClient BaseClient()
        {
            var client = new RestClient(BaseUrl);
            client.AddHandler("application/json", RestSharp.Serializers.Newtonsoft.Json.NewtonsoftJsonSerializer.Default);

            return client;
        }

        public RestRequest BaseRequest(string url, Method method)
        {
            var request = new RestRequest(url, method)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new RestSharp.Serializers.Newtonsoft.Json.NewtonsoftJsonSerializer()
            };

            return request;
        }

        public async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            var startTime = DateTime.UtcNow;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var response = await BaseClient().ExecuteTaskAsync<T>(request);

            watch.Stop();

            var requestLog = RequestLog.FromResponse(startTime, watch.ElapsedMilliseconds, response);

            await _requestLogRepository.Add(requestLog);

            return response;
        }

        public void ExecuteTask(IRestRequest request)
        {
            var startTime = DateTime.UtcNow;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var response = BaseClient().Execute(request);

            watch.Stop();

            var requestLog = RequestLog.FromResponse(startTime, watch.ElapsedMilliseconds, response);

            _requestLogRepository.Add(requestLog);
        }
    }
}
