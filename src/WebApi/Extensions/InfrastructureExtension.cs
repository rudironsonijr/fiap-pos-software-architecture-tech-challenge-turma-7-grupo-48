using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;

namespace WebApi.Extensions;

internal static class InfrastructureExtension
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		return services
			.AddSqlRepositories()
			.AddAdapters();
	}

	private static IServiceCollection AddSqlRepositories(this IServiceCollection services)
	{
		return services
			.AddScoped<ICustomerSqlRepository, CustomerSqlRepository>();
	}

	private static IServiceCollection AddAdapters(this IServiceCollection services)
	{
		return services
			.AddScoped<ICustomerRepository, CustomerRepositoryAdapter>();
	}
}