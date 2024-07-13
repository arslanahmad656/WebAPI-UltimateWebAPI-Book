using Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CompanyEmployees.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void UseErrorHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature is not null)
                {
                    var error = errorFeature.Error;
                    logger.LogError($"Error occurred while processing a request. ${error}");

                    context.Response.StatusCode = errorFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        Message = error.Message,
                        StatusCode = context.Response.StatusCode
                    }.ToString()).ConfigureAwait(false);
                }
            });
        });
    }
}
