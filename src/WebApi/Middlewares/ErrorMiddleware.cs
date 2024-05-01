using Domain.Entities.Exceptions;
using Domain.ValueObjects.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace WebApi.Middlewares;

[ExcludeFromCodeCoverage]
public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    private readonly static JsonSerializerOptions _serializerOptions = new();

    public ErrorMiddleware(
        RequestDelegate next,
        ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;

    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        when (exception
        is InvalidEmailException
        or InvalidCpfException
        or EntityArgumentNullException)
        {
            _logger.LogWarning(exception, exception.Message);

            var statusCodeResponse = HttpStatusCode.BadRequest;
            var problemDetails = BuildProblemDetails(context.Request.Path, exception.Message, statusCodeResponse);
            await HandleResponseAsync(context, (int)statusCodeResponse, problemDetails);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var statusCodeResponse = HttpStatusCode.BadRequest;
            var problemDetails = BuildProblemDetails(context.Request.Path, exception.Message, statusCodeResponse);
            await HandleResponseAsync(context, (int)statusCodeResponse, problemDetails);
        }
    }

    private static async Task HandleResponseAsync<T>(HttpContext context, int statusCodeResponse, T response)
    {
        context.Response.StatusCode = statusCodeResponse;
        await context.Response.WriteAsJsonAsync(response, options: _serializerOptions);
    }
    private static ProblemDetails BuildProblemDetails(string instace, string detail, HttpStatusCode statusCode)
    {
        return new ProblemDetails
        {
            Title = statusCode.ToString(),
            Instance = instace,
            Status = (int)statusCode,
            Type = "about:blank"
        };
    }
}
