using Domain.Entities.CustomerAggregate;
using Infrastructure.SqlModels;

namespace Infrastructure.Extensions.Entities;

internal static class CustomerExtension
{
	public static CustomerSqlModel ToCustomerSqlModel(this Customer customer)
	{
		CustomerSqlModel response = new()
		{
			Id = customer.Id, 
			Name = customer.Name, 
			Cpf = customer.Cpf, 
			Email = customer.Email
		};

		return response;
	}
}