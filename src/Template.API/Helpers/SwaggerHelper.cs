using System;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.WebApi;

namespace Template.API.Helpers
{
    /// <summary>
    /// Helper class for API Documentation
    /// </summary>
    public class SwaggerHelper
    {
        // Swagger const
        private const string SWAGGER_JSON_ROUTE_UI3 = "/swagger/v1/swagger.json";
        private const string SWAGGER_UI3_ROUTE = "/swagger";
        private const string SWAGGER_DOCUMENT_NAME = "v1";

        // Redoc consts
        private const string SWAGGER_REDOC_ROUTE = "/redoc";

        // Common consts
        private const string DESCRIPTION = "Red Bull Ruby API";
        private const string VERSION = "1.0.0";
        private const string TITLE = "RBR";

        // Swagger authorization
        private const string SWAGGER_HEADER_TITLE = "HEADER";
        private const string SWAGGER_HEADER_DESCRIPTION = "You can use authorization tokens";
        private const string SWAGGER_HEADER_NAME = "Authorization";

        /// <summary>
        /// Configures Swagger
        /// </summary>
        /// <returns></returns>
        public static Action<SwaggerDocumentMiddlewareSettings> SwaggerConfigure()
        {
            return configure =>
            {
                configure.DocumentName = SWAGGER_DOCUMENT_NAME;
                configure.PostProcess = (document, _) =>
                {
                    document.Schemes = new[] { SwaggerSchema.Https };
                    document.SecurityDefinitions.Add(SWAGGER_HEADER_TITLE, new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = SWAGGER_HEADER_NAME,
                        In = SwaggerSecurityApiKeyLocation.Header,
                        Description = SWAGGER_HEADER_DESCRIPTION
                    }
                    );
                    document.Info.Description = DESCRIPTION;
                    document.Info.Version = VERSION;
                    document.Info.Title = TITLE;
                };

                configure.DocumentName = SWAGGER_DOCUMENT_NAME;
            };
        }

        /// <summary>
        /// Configures Swagger UI
        /// </summary>
        /// <returns></returns>
        public static Action<SwaggerUi3Settings<WebApiToSwaggerGeneratorSettings>> SwaggerUi3Configure() => configure => configure.Path = SWAGGER_UI3_ROUTE;

        /// <summary>
        /// Configures ReDoc
        /// </summary>
        /// <returns></returns>
        public static Action<SwaggerReDocSettings<WebApiToSwaggerGeneratorSettings>> ReDocConfigure()
        {
            return configure =>
            {
                configure.Path = SWAGGER_REDOC_ROUTE;
                configure.DocumentPath = SWAGGER_JSON_ROUTE_UI3;
            };
        }
    }
}
