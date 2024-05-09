using Application.Services;
using Application.Services.Interfaces;
using Core.Notifications;

namespace WebApi.Extensions;

public static class ApplicationExtension
{
	public static IServiceCollection AddAplication(this IServiceCollection services)
	{
		return
			services
				.AddServices()
				.AddNotifications();
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services
			.AddScoped<IProductService, ProductService>()
			.AddScoped<IOrderService, OrderService>();
	}

	private static IServiceCollection AddNotifications(this IServiceCollection services)
	{
		return services
			.AddScoped<NotificationContext>();
	}
}