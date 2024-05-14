using Domain.Repositories;
using Infrastructure.Adapters;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

internal static class InfrastructureExtension
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddSqlRepositories()
			.AddAdapters()
			.AddContext(configuration);
	}

	private static IServiceCollection AddSqlRepositories(this IServiceCollection services)
	{
		return services
			.AddScoped<ICustomerSqlRepository, CustomerSqlRepository>();
	}

	private static IServiceCollection AddAdapters(this IServiceCollection services)
	{
		return services
			.AddScoped<ICustomerRepository, CustomerRepositoryAdapter>()
			.AddScoped<IProductRepository, ProductRepositoryAdapter>()
			.AddScoped<IOrderRepository, OrderRepositoryAdpater>();
	}

	private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
	{
		return
			services
				.AddScoped<DinersSqlContext>()
				.AddDbContext<DinersSqlContext>(opts =>
					opts.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
	}
}