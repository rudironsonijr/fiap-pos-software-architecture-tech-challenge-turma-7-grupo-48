using Domain.Gateways;
using Integration.Gateway;
using Integration.Strategies.Pix;
using Integration.Strategies.Pix.Interface;

namespace WebApi.Extensions;

public static class IntegrationExtension
{
	public static IServiceCollection AddIntegration(this IServiceCollection services)
	{
		return 
			services.AddStrategies()
				.AddResolver()
				.AddGateway();
	}

	private static IServiceCollection AddGateway(this IServiceCollection services)
	{
		return 
			services.AddScoped<IPaymentGateway, PaymentGateway>();
	}

	private static IServiceCollection AddResolver(this IServiceCollection services)
	{
		return
			services.AddScoped<IPixStrategyResolver, PixStrategyResolver>();
	}

	private static IServiceCollection AddStrategies(this IServiceCollection services)
	{
		return
			services.AddScoped<IPixPaymentStrategy, MercadoPagoPixStrategy>();
	}
}
