using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using ILogger = Serilog.ILogger;

namespace Customer.API.Middlewares;

public class ModelValidationMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await next(context);
            return;
        }

        var handlerMethod = endpoint.Metadata.OfType<MethodInfo>().FirstOrDefault();
        if (handlerMethod == null)
        {
            await next(context);
            return;
        }

        // Lấy danh sách các tham số của phương thức handler
        var parameters = handlerMethod.GetParameters();
        foreach (var parameter in parameters)
        {
            // Kiểm tra chỉ các tham số là DTO (kiểu class, không phải interface hoặc abstract class)
            if (parameter.ParameterType.IsClass && !parameter.ParameterType.IsInterface && !parameter.ParameterType.IsAbstract)
            {
                // Kiểm tra nếu Content-Type là 'application/json'
                if (!context.Request.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) ?? true)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        errors = new[] { "Invalid Content-Type. Expected 'application/json'." }
                    });
                    return;
                }

                object? body = null;
                try
                {
                    // Chỉ deserialize nếu tham số là DTO
                    body = await context.Request.ReadFromJsonAsync(parameter.ParameterType);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        errors = new[] { $"Invalid JSON format: {ex.Message}" }
                    });
                    return;
                }

                if (body == null)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        errors = new[] { "Request body cannot be null or empty." }
                    });
                    return;
                }

                // Kiểm tra validation của DTO (nếu có các ValidationAttributes)
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(body);
                if (!Validator.TryValidateObject(body, validationContext, validationResults, true))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        errors = validationResults.Select(v => v.ErrorMessage)
                    });
                    return;
                }
            }
        }

        // Nếu không có lỗi, tiếp tục xử lý yêu cầu
        await next(context);
    }
}