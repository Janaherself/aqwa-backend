using System.Text.Json;
using GymManagement.Application.Common.Errors;

namespace GymManagement.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var error = new AppError("InternalServerError", "An unexpected error occurred.");
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
