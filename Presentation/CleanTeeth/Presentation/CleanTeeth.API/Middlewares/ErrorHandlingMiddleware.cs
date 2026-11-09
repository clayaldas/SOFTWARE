using CleanTeeth.Application.Exceptions;
using CleanTeeth.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace CleanTeeth.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private const string InternalErrorMessage = "Ocurrió un error interno.";

    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
        }
        catch (CustomValidationException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (BusinessRuleException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (MediatorException ex)
        {
            _logger.LogError(ex, "Error de configuración del Mediator.");
            await HandleExceptionAsync(
                context, InternalErrorMessage, HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción no controlada.");
            await HandleExceptionAsync(
                context, InternalErrorMessage, HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        string message,
        HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var response = new { error = message };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}


public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
