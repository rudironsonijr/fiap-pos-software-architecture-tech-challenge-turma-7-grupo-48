using Controller.Dtos.CustomerResponse;
using Domain.Entities.CustomerAggregate;

namespace Controller.Extensions.CustomerAggregate;

internal static class CustomerExtension
{
	public static CreateCustomerResponse ToCreateCustomerResponse(this Customer customer)
	{
		return new()
		{
			CustomerId = customer.Id,
		};
	}
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
