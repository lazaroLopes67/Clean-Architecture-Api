using Criando_Minha_Primeira_API.Model;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Criando_Minha_Primeira_API.Extension
{
    /// <summary>
    /// Provides extension methods to configure global exception handling
    /// for the ASP.NET Core request pipeline.
    /// </summary>
    public static class ExtesionMethodException
    {
        /// <summary>
        /// Registers a global exception handler middleware that catches all unhandled exceptions
        /// and returns a standardized JSON error response to the client.
        /// </summary>
        /// <param name="app">
        /// The application builder used to configure the HTTP request pipeline.
        /// </param>
        public static void ConfigureExceptionsHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appErr =>
            {
                appErr.Run(async context =>
                {
                    // Sets HTTP status code to 500 (Internal Server Error)
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    // Defines response content type as JSON
                    context.Response.ContentType = "application/json";

                    // Retrieves exception details from the current HTTP context
                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeatures != null)
                    {
                        // Determines whether the application is running in Development mode
                        var isDevelopment = app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment();

                        // Builds a structured error response
                        var errorResponse = new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,

                            // Exception message returned to the client
                            Message = contextFeatures.Error.Message,

                            // Stack trace is only exposed in Development mode
                            Trace = isDevelopment ? contextFeatures.Error.StackTrace : null
                        };

                        // Writes the error response as JSON to the HTTP response
                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }
                });
            });
        }
    }
}