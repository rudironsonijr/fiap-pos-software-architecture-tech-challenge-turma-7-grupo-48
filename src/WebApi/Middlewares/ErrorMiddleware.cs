using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using Domain.Entities.Exceptions;
using Domain.ValueObjects.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middlewares;

[ExcludeFromCodeCoverage]
public class ErrorMiddleware
{
	private static readonly JsonSerializerOptions _serializerOptions = new();
	private readonly ILogger<ErrorMiddleware> _logger;
	private readonly RequestDelegate _next;

	public ErrorMiddleware(
		RequestDelegate next,
		ILogger<ErrorMiddleware> logger
	)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context: context);
		}
		catch (Exception exception)
			when (exception
				      is InvalidEmailException
				      or InvalidCpfException
				      or EntityArgumentException)
		{
			_logger.LogWarning(
				exception: exception,
				message: exception.Message
			);

			var statusCodeResponse = HttpStatusCode.BadRequest;
			var problemDetails = BuildProblemDetails(
				instace: context.Request.Path,
				detail: exception.Message,
				statusCode: statusCodeResponse
			);
			await HandleResponseAsync(
				context: context,
				statusCodeResponse: (int)statusCodeResponse,
				response: problemDetails
			);
		}
		catch (Exception exception)
		{
			_logger.LogError(
				exception: exception,
				message: exception.Message
			);

			var statusCodeResponse = HttpStatusCode.BadRequest;
			var problemDetails = BuildProblemDetails(
				instace: context.Request.Path,
				detail: exception.Message,
				statusCode: statusCodeResponse
			);
			await HandleResponseAsync(
				context: context,
				statusCodeResponse: (int)statusCodeResponse,
				response: problemDetails
			);
		}
	}

	private static async Task HandleResponseAsync<T>(
		HttpContext context,
		int statusCodeResponse,
		T response
	)
	{
		context.Response.StatusCode = statusCodeResponse;
		await context.Response.WriteAsJsonAsync(
			value: response,
			options: _serializerOptions
		);
	}

	private static ProblemDetails BuildProblemDetails(
		string instace,
		string detail,
		HttpStatusCode statusCode
	)
	{
		return new ProblemDetails
		{
			Title = statusCode.ToString(), Instance = instace, Status = (int)statusCode, Type = "about:blank"
		};
	}
}