using Core.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;
using WebApi.Factories;

namespace WebApi.Filters;


public class NotificationFilter : IAsyncResultFilter
{
	private readonly NotificationContext _notificationContext;

	public NotificationFilter(NotificationContext notificationContext)
	{
		_notificationContext = notificationContext;
	}

	public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
	{
		if (_notificationContext.HasErrors)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.HttpContext.Response.ContentType = "application/json";

			var problemDetails = ProblemDetailsFactory.BuildProblemDetails(context.HttpContext.Request.Path, _notificationContext.Errors, HttpStatusCode.BadRequest);
			var notifications = JsonSerializer.Serialize(problemDetails);
			await context.HttpContext.Response.WriteAsync(notifications);

			return;
		}

		await next();
	}
}
