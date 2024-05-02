using Application.Services;
using Application.Services.Interfaces;

namespace WebApi.Extensions
{
	public static class ApplicationExtension
	{
		public static IServiceCollection AddAplication(this IServiceCollection services)
		{
			return
				services.AddServices();
		}

		private static IServiceCollection AddServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IProductService, ProductService>();

		}
	}
}
