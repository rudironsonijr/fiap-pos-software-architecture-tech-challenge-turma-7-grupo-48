using Domain.Entities.CustomerAggregate;
using Infrastructure.SqlModels.CustomerAggregate;

namespace Infrastructure.SqlModels.CustomerAggregate.Extensions;

internal static class CustomerSqlModelExtension
{
	public static Customer ToCustomer(this CustomerSqlModel model)
	{
		Customer response = new()
		{
			Id = model.Id,
			Name = model.Name,
			Cpf = model.Cpf,
			Email = model.Email
		};

		return response;
	}
}