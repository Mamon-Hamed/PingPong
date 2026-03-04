using System.Text.Json;
using PingPong.Application.Common;
using PingPong.Domain.Exceptions;
using ValidationException = PingPong.Domain.Exceptions.ValidationException;

namespace PingPong.API.Middleware;

public sealed class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, response) = exception switch
        {
            ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                ApiResponse.Fail(400, validationEx.Message, validationEx.Errors)
            ),
            BadRequestException badRequestEx => (
                StatusCodes.Status400BadRequest,
                ApiResponse.Fail(400, badRequestEx.Message)
            ),
            NotFoundException notFoundEx => (
                StatusCodes.Status404NotFound,
                ApiResponse.Fail(404, notFoundEx.Message)
            ),
            ConflictException conflictEx => (
                StatusCodes.Status409Conflict,
                ApiResponse.Fail(409, conflictEx.Message)
            ),
            ForbiddenAccessException forbiddenEx => (
                StatusCodes.Status403Forbidden,
                ApiResponse.Fail(403, forbiddenEx.Message)
            ),
            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                ApiResponse.Fail(401, "You are not authorized to access this resource.")
            ),
            OperationCanceledException => (
                StatusCodes.Status499ClientClosedRequest,
                ApiResponse.Fail(499, "The request was cancelled.")
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                ApiResponse.Fail(500, "An unexpected error occurred. Please try again later.")
            )
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, "Handled exception: {ExceptionType} - {Message}",
                exception.GetType().Name, exception.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }
}
