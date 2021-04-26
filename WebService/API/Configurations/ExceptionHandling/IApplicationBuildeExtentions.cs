using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace API.Configurations.ExceptionHandling
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure's Exception handler.
        /// </summary>
        /// <param name="applicationBuilder">Configuration of application.</param>
        /// <param name="webHostEnvironment"><see cref="IWebHostEnvironment"/> which contains information of current enviroment.</param>
        /// <returns>Instance of the configured <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder ConfigureExceptionHandler(
            this IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {
            if (applicationBuilder == null)
                throw new ArgumentNullException(nameof(applicationBuilder));

            applicationBuilder.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    // Get information of the exception which occured.
                    var exceptionHandlerPathFeature = context.Features
                        .Get<IExceptionHandlerPathFeature>();

                    // If the isn't handled before this code, we will return internal server,
                    // because we were not expecting this error to happen.
                    
                        if (env.IsProduction())
                        {
                            // Prepare error message object to write.
                            var error = new Error()
                            {
                                Code = "InternalServerError",
                                Message = "An unkown error occured while processing the request."
                            };
                        }

                        if (env.IsDevelopment())
                        {
                            var innerError = new ErrorModel.InnerError()
                            {
                                Code = "InternalServerError",
                                Message = exceptionHandlerPathFeature.Error.StackTrace
                            };


                            // Prepare error message object to write.
                            var error = new ErrorModel()
                            {
                                Code = "InternalServerError",
                                Message = exceptionHandlerPathFeature.Error.Message,
                                Details = new List<ErrorModel.InnerError>()
                                {
                                 innerError
                                }
                            };
                       
                        // Set content type to json as response type.
                        context.Response.ContentType = "application/json";

                        // Status code.
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        // Wait error as json.
                        await context.Response.WriteAsync(JsonSerializer.Serialize(error, jsonSerializerOptions));

                        return;
                    }
                });
            });

            applicationBuilder.UseMiddleware(typeof(BadRequestExceptionMiddleware));

            applicationBuilder.UseMiddleware(typeof(NotFoundExceptionMiddleware));

            applicationBuilder.UseMiddleware(typeof(FluentValidationExceptionMiddleware));

            return applicationBuilder;
        }

        public class Error
        {
            /// <summary>
            /// One of the server defined error codes.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// A human-readable representation of the error.
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// List of errors which were the reason for the error.
            /// </summary>
            public IEnumerable<InnerError> Details { get; set; }
                = new List<InnerError>();

            public class InnerError
            {
                /// <summary>
                /// One of the server defined error codes.
                /// </summary>
                public string Code { get; set; }

                /// <summary>
                /// A human-readable representation of the error.
                /// </summary>
                public string Message { get; set; }

                /// <summary>
                /// Target of the error.
                /// </summary>
                public string Target { get; set; }
            }
        }
    }
}
