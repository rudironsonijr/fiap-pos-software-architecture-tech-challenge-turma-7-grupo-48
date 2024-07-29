using Controller.Application;
using Controller.Application.Interfaces;
using Controller.Application.Interfaces;
using Integration.Controller.Interfaces;

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
			.AddScoped<IProductApplication, ProductApplication>()
			.AddScoped<IPaymentApplication, PaymentApplication>()
			.AddScoped<IMercadoPagoApplication, MercadoPagoApplication>();

	}
}
