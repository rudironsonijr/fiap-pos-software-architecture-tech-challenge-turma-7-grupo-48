using UseCase.Services.Interfaces;
using UseCase.Services;
using Controller.Application;
using Controller.Application.NewFolder;
using Controller.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions;

public static class ControllerLayerExtension
{
	public static IServiceCollection AddControllerLayerDI(this IServiceCollection services)
	{
		return
			services
				.AddApplication();
	}

	private static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddScoped<ICustomerApplication, CustomerApplication>()
			.AddScoped<IOrderApplication, OrderApplication>()
			.AddScoped<IProductApplication, ProductApplication>();

	}
}
