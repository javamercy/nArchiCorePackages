using System.Text.Json;
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ExceptionMiddleware(RequestDelegate next, LoggerServiceBase loggerServiceBase,
        IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _loggerServiceBase = loggerServiceBase;
        _httpContextAccessor = httpContextAccessor;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogExceptionAsync(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task LogExceptionAsync(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter { Type = context.GetType().Name, Value = exception.ToString() }
        };

        LogDetailWithException logDetailWithException = new()
        {
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?",
            ExceptionMessage = exception.Message
        };

        _loggerServiceBase.Error(JsonSerializer.Serialize(logDetailWithException));

        return Task.CompletedTask;
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;

        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}
