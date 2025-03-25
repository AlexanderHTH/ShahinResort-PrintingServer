using PrintingServer.Domain.Common;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.API.MiddleWares;
public class ErrorHandlingMiddleWare(ITQLogger<ErrorHandlingMiddleWare> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogInformation("Request received: {Path}", context.Request.Path);
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            logger.LogWarning(notFound.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(notFound.Message);
        }
        catch (AlreadyFoundException found)
        {
            logger.LogWarning(found.Message);
            context.Response.StatusCode = StatusCodes.Status302Found;
            await context.Response.WriteAsync(found.Message);
        }
        catch (HandlingDataException handlingData)
        {
            logger.LogWarning(handlingData.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(handlingData.Message);
        }
        catch (FluentValidationException fluentValidation)
        {
            string Errors = string.Join($"; {Environment.NewLine}", fluentValidation.Errors.Select(e => e.ErrorMessage));
            string msg = $"Validation failed:{Environment.NewLine}{Errors}";
            logger.LogWarning(msg);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorResponse = new
            {
                Message = "Validation failed.",
                Errors = fluentValidation.ToDictionary()
            };
            
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        catch (ForbidException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync("Access Forbidden.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"Somthing went wrong:{Environment.NewLine}- {ex.Message}");
        }
    }
}
