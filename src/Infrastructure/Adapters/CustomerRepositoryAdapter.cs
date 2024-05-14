using Domain.Entities.CustomerAggregate;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Extensions.Entities;
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

	public async Task<Customer?> GetByCpf(Cpf cpf, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.GetAsync(
			customer => customer.Cpf.Equals(cpf.FormatedNumber),
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

	public Task<int> CreateAsync(Customer customer, CancellationToken cancellationToken)
	{
		var customerSql = customer.ToCustomerSqlModel();
		_customerSqlRepository.Add(customerSql);
		return _customerSqlRepository.UnitOfWork.CommitAsync(cancellationToken);

	}

	public async Task<bool> ExistsByCpf(Cpf cpf, CancellationToken cancellationToken)
	{
		var customer = await _customerSqlRepository.CountAsync(x => x.Cpf.Equals(cpf), cancellationToken);
		return customer > 0;
	}
}