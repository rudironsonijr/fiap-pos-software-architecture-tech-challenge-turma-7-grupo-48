using Domain.Entities.CustomerAggregate;
using Domain.Repositories;
using Domain.Repositories.Base;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.Extensions;

namespace Infrastructure.Repositories;

public class CustomerRepositoryAdapter : ICustomerRepository
{
	private readonly ICustomerSqlRepository _customerSqlRepository;

	public IUnitOfWork UnitOfWork => _customerSqlRepository.UnitOfWork;

	public CustomerRepositoryAdapter(ICustomerSqlRepository customerSqlRepository)
	{
		_customerSqlRepository = customerSqlRepository;
	}

	public async Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.GetAsync(
			customer => customer.Cpf.Equals(cpf),
			cancellationToken);

		return customerSql?.ToCustomer();
	}

	public async Task<Customer?> GetAsync(string id, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.GetAsync(
			customer => customer.Id.Equals(id),
			cancellationToken);

		return customerSql?.ToCustomer();
	}

	public async Task<Customer> CreateAsync(Customer id, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.
		throw new NotImplementedException();
	}

	public Task<bool> ExistsByCpf(string? cpf, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}