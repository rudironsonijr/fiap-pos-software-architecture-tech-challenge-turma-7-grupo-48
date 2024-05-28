using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Factories;

public static class ProblemDetailsFactory
{
	internal static ProblemDetails BuildProblemDetails(string instace, string detail, HttpStatusCode statusCode)
	{
		return new ProblemDetails
		{
			Title = statusCode.ToString(),
			Instance = instace,
			Status = (int)statusCode,
			Type = "about:blank",
			Detail = detail
		};
	}

	internal static ProblemDetails BuildProblemDetails(string instace, IEnumerable<string> errorDetails, HttpStatusCode statusCode)
	{
		var detail = string.Join($", {Environment.NewLine} ", errorDetails);
		return new ProblemDetails
		{
			Title = statusCode.ToString(),
			Instance = instace,
			Status = (int)statusCode,
			Type = "about:blank",
			Detail = detail
		};
	}

	internal static ProblemDetails BuildProblemDetails(string instace, HttpStatusCode statusCode)
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
