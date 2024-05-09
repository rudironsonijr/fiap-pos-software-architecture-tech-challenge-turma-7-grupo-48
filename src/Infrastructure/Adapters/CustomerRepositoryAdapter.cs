using Domain.Entities.CustomerAggregate;
using Domain.Repositories;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.Extensions;

namespace Infrastructure.Repositories;

public class CustomerRepositoryAdapter : ICustomerRepository
{
	private readonly ICustomerSqlRepository _customerSqlRepository;

	public CustomerRepositoryAdapter(ICustomerSqlRepository customerSqlRepository)
	{
		_customerSqlRepository = customerSqlRepository;
	}

	public async Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.GetAsync(
			customer => customer.Cpf.Equals(cpf),
			cancellationToken
		);

		return customerSql?.ToCustomer();
	}

	public async Task<Customer?> GetAsync(string id, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.GetAsync(
			customer => customer.Id.Equals(id),
			cancellationToken
		);

		return customerSql?.ToCustomer();
	}
}