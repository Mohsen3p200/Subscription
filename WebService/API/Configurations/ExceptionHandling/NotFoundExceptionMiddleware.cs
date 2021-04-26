using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Configurations.ExceptionHandling
{
    public sealed class NotFoundExceptionMiddleware
    {
        /// <summary>
        /// <see cref="RequestDelegate"/> to use for calling the next
        /// middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        public NotFoundExceptionMiddleware(RequestDelegate next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));

            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            try
            {
                // Call the next middleware in the pipeline.
                await _next(httpContext);
            }
            catch (NotFoundException)
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Prepare error message object to write.
                var error = new ErrorModel()
                {
                    Code = "NotFound",
                    Message = "The requested resource does not exist.",
                    Details = new List<ErrorModel.InnerError>()
                };

                // Set content type to json as response type.
                httpContext.Response.ContentType = "application/json";

                // Status code.
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                // Wait error as json.
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(error,
                    jsonSerializerOptions));
            }
        }
    }
    }
