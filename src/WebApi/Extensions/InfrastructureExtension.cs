using Domain.Repositories;
using Infrastructure.Adapters;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

internal static class InfrastructureExtension
{
	private static string ConnectionString;

	static InfrastructureExtension()
	{
		ConnectionString = GetConnectionString();
	}
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
			.AddScoped<IOrderProductRepository, OrderProductRepository>()
			.AddScoped<ICustomerSqlRepository, CustomerSqlRepository>()
			.AddScoped<IOrderSqlRepository, OrderSqlRepository>()
			.AddScoped<IProductSqlRepository, ProductSqlRepository>()
			.AddScoped<IPaymentSqlModelRepository, PaymentSqlModelRepository>();
	}

	private static IServiceCollection AddAdapters(this IServiceCollection services)
	{
		return services
			.AddScoped<ICustomerRepository, CustomerRepositoryAdapter>()
			.AddScoped<IProductRepository, ProductRepositoryAdapter>()
			.AddScoped<IOrderRepository, OrderRepositoryAdpater>()
			.AddScoped<IPaymentRepository, PaymentRepositoryAdpater>();
	}

	private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
	{
		return
			services
				.AddScoped<DinersSqlContext>()
				.AddDbContext<DinersSqlContext>(opts =>
					opts.UseSqlServer(ConnectionString));
	}

	public static void MigrationInitialisation(this IApplicationBuilder app)
	{
		Console.WriteLine("Iniciando migration");
		try
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				var db = serviceScope.ServiceProvider.GetRequiredService<DinersSqlContext>();

				if (db.Database.GetPendingMigrations().Any())
				{
					db.Database.Migrate();
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}

		Console.WriteLine("Migration finalizada");
	}

	private static string GetConnectionString()
	{
		var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

		if (!string.IsNullOrEmpty(connectionString))
		{
			return connectionString;
		}

		throw new Exception("Enviroment Variable DefaultConnection not found ");
	}

}