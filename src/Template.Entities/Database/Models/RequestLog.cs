using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using RestSharp;
using Template.Entities.Database.Base;

namespace Template.Entities.Database.Models
{
    public class RequestLog : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime RequestSentDate { get; set; }
        public DateTime ResponseReceivedDate { get; set; }

        public string Request { get; set; }
        public string RequestUri { get; set; }

        public string Response { get; set; }
        public long ResponseElapsedMilliseconds { get; set; }
        public string ResponseUri { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorException { get; set; }

        public RequestLog() { }

        public static RequestLog FromResponse(DateTime start, long elapsed, IRestResponse response)
        {
            return new RequestLog
            {
                RequestSentDate = start,
                Request = JsonConvert.SerializeObject(response.Request.Parameters).ToString(),
                RequestUri = response.Request.Resource,
                ResponseElapsedMilliseconds = elapsed,
                ResponseReceivedDate = start.AddMilliseconds(elapsed),
                Response = response.Content,
                ResponseUri = response.ResponseUri.ToString(),
                ResponseStatus = response.ResponseStatus.ToString(),
                ResponseCode = response.StatusCode.ToString(),
                ErrorMessage = response.ErrorMessage ?? string.Empty,
                ErrorException = response.ErrorException?.ToString() ?? string.Empty
            };
        }
    }
}
