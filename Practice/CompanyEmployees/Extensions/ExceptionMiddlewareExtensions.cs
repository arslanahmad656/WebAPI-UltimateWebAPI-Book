using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CompanyEmployees.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void UseErrorHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature is not null)
                {
                    var error = errorFeature.Error;
                    logger.LogError($"Error occurred while processing a request. ${error}");

                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        Message = "Internal Server Error",
                        StatusCode = (HttpStatusCode)context.Response.StatusCode
                    }.ToString()).ConfigureAwait(false);
                }
            });
        });
    }
}
