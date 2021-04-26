using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Configurations.ExceptionHandling
{
    public sealed class FluentValidationExceptionMiddleware
    {
        /// <summary>
        /// <see cref="RequestDelegate"/> to use for calling the next
        /// middlware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// <see cref="ILogger"/> to use for logging information.
        /// </summary>
        private readonly ILogger<FluentValidationExceptionMiddleware> _logger;

        public FluentValidationExceptionMiddleware(RequestDelegate next,
            ILogger<FluentValidationExceptionMiddleware> logger)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));
            try
            {
                // Call the next middlware in the pipeline.
                await _next(httpContext);
            }
            catch (ValidationException validationException)
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Prepare inner error message objects to write.
                var innerErrors = validationException.Errors?.Select(x => new ErrorModel.InnerError()
                {
                    Code = x.ErrorCode,
                    Message = x.ErrorMessage,
                    Target = x.PropertyName
                });

                // Prepare error message object to write.
                var error = new ErrorModel()
                {
                    Code = "BadArguments",
                    Message = "One or more bad arguments",
                    Details = innerErrors
                };

                _logger.LogDebug("Bad request occured with message {message}.", JsonSerializer.Serialize(error));

                // Set content type to json as response type.
                httpContext.Response.ContentType = "application/json";

                // Status code.
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // Wait error as json.
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(error, jsonSerializerOptions));
            }
        }
    }
}
