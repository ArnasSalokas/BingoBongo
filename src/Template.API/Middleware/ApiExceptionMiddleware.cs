using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Template.Common.Exceptions;
using Template.Common.Extensions;
using Template.Entities.API.Models.Base;

namespace Template.API.Middleware
{
    /// <summary>
    /// Main API exception handling middleware
    /// </summary>
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public ApiExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _next = next;
            _env = env;
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (NotImplementedException ex) when (_env.IsDevelopment())
            {
                context.Response.StatusCode = StatusCodes.Status501NotImplemented;

                var apiError = new ApiErrorResponse
                {
                    Code = Convert.ToInt32(MpExceptionCode.General.NotImplemented),
                    Message = ex.Message
                };

                SetResponseBody(context, apiError);
            }

            catch (MpException ex)
            {
                _logger.LogTrace(ex, $"'{context.Request.Path}' '{ex.Message}'");

                if (ex.Level == LogLevel.Information)
                    _logger.LogInformation(ex, ex.Message);

                if (ex.Level == LogLevel.Warning)
                    _logger.LogWarning(ex, ex.Message);

                if (ex.Level == LogLevel.Error)
                    _logger.LogError(ex, ex.Message);

                if (ex.Level == LogLevel.Critical)
                    _logger.LogCritical(ex, ex.Message);

                if (ex.Code.Equals(MpExceptionCode.General.Unauthorized))
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                else
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                SetResponseBody(context, new ApiErrorResponse
                {
                    Code = Convert.ToInt32(ex.Code),

                    Fields = ex.FieldExceptions
                        .Select(e => new ApiErrorFieldResponse(e.Field, Convert.ToInt32(e.Code), e.Message, e.Value)),

                    Message = ex.Message
                });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"'{context.Request.Path}' '{ex.Message}'");

                if (_env.IsDevelopment()) throw;

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new ApiErrorResponse
                {
                    Code = (int)MpExceptionCode.General.UnknownError,
                    Message = MpExceptionCode.General.UnknownError.GetDescription()
                };

                if (_env.IsDevelopment())
                {
                    response.Message = ex.Message;
                    response.StackTrace = ex.StackTrace;
                }

                SetResponseBody(context, response);
            }
        }

        private void SetResponseBody(HttpContext context, ApiErrorResponse response)
        {
            var json = JsonConvert.SerializeObject(
                response,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (var streamWriter = new StreamWriter(context.Response.Body))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }
        }
    }
}
