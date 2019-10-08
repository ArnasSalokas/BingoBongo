using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Template.Common.Exceptions;
using Template.Services.Base;
using Template.Services.Exporting.Contracts;

namespace Template.API.Controllers.Base
{
    /// <summary>
    /// Base web controller
    /// </summary>
    public abstract class BaseWebController : BaseController
    {
        private const string XLS = "application/vnd.ms-excel";

        /// <summary>
        /// Excel export
        /// </summary>
        protected readonly IExcelExportService _excelExportService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cache"></param>
        public BaseWebController(IServiceProvider services, IMemoryCache cache) : base(services, cache)
        {
            _excelExportService = services.GetExcelExportService();
        }

        /// <summary>
        /// Validates model
        /// </summary>
        /// <param name="model"></param>
        [NonAction]
        public void ValidateModel(object model)
        {
            var results = new List<ValidationResult>();

            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);

            if (results.Count != 0)
            {
                var str = string.Empty;
                foreach (var result in results)
                {
                    str += result.ErrorMessage;
                    str += Environment.NewLine;
                }

                throw new MpException(MpExceptionCode.General.DataValidationFailed);
            }
        }

        /// <summary>
        /// Returns current user id
        /// </summary>
        [NonAction]
        public int? GetCurrentUserId() => GetCurrentUserSessionToken()?.UserId;

        /// <summary>
        /// Exports records to XLS file
        /// </summary>
        /// <param name="records"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [NonAction]
        public FileContentResult ToXls<T>(IEnumerable<T> records, string name = "records") => File(_excelExportService.Export<T>(name, records.ToList()), XLS, $"{name}.xlsx");
    }
}
