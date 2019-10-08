using System.Collections.Generic;

namespace Template.Entities.API.Models.Base
{
    public class ApiErrorResponse : IApiErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public IEnumerable<ApiErrorFieldResponse> Fields { get; set; }
        public string StackTrace { get; set; }

        public ApiErrorResponse() { }

        public ApiErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public class ApiErrorFieldResponse : IApiErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string Field { get; set; }
        public string Value { get; set; }

        public ApiErrorFieldResponse() { }

        public ApiErrorFieldResponse(string field, int code, string message, string value)
        {
            Field = field;
            Code = code;
            Message = message;
            Value = value;
        }
    }

    public interface IApiErrorResponse
    {
        int Code { get; set; }
        string Message { get; set; }
    }
}
