using Application.Dtos.CustomerResponse;
using Domain.Entities.CustomerAggregate;

namespace Application.Extensions.CustomerAggregate;

internal static class CustomerExtension
{
	public static GetCustomerResponse ToGetCustomerResponse(this Customer customer)
	{
		GetCustomerResponse response = new()
		{
			Id = customer.Id,
			Name = customer.Name,
			Cpf = customer.Cpf,
			Email = customer.Email,
		};

		return response;
	}
}
