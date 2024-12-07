using System.Net;
using System.Text.Json;
using Customer.API.Exceptions;
using ILogger = Serilog.ILogger;
namespace Customer.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger logger)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException e)
        {
            logger.Warning("Resource not found");
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.NotFound);
        }
        catch (InvalidOperationException ex)
        {
            logger.Warning(ex, "Invalid operation");
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (ExistingFieldException ex)
        {
            logger.Warning(ex, "Existing field");
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        var response = new
        {
            message = exception.Message,
            statusCode = (int)statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}