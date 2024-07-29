using Domain.Entities.CustomerAggregate;
using Domain.Repositories;
using Domain.Repositories.Base;
using Domain.ValueObjects;
using Infrastructure.Extensions.CustomerAggregate;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.CustomerAggregate.Extensions;

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
		var customerSql = await _customerSqlRepository.GetAsync(customer => customer.Cpf.Equals(cpf.FormatedNumber),
			false, cancellationToken);

		return customerSql?.ToCustomer();
	}

	public async Task<Customer?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var customerSql = await _customerSqlRepository.GetAsync(customer => customer.Id.Equals(id),
			false, cancellationToken);

		return customerSql?.ToCustomer();
	}

	public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
	{
		var customerSql = customer.ToCustomerSqlModel();
		_customerSqlRepository.Add(customerSql);
		await _customerSqlRepository.UnitOfWork.CommitAsync(cancellationToken);

		return new()
		{
			Id = customerSql.Id,
			Name = customer.Name,
			Email = customer.Email,
			Cpf = customer.Cpf,
		};
		

	}

	public async Task<bool> ExistsByCpf(Cpf cpf, CancellationToken cancellationToken)
	{
		var customer = await _customerSqlRepository.CountAsync(x => x.Cpf.Equals(cpf), cancellationToken);
		return customer > 0;
	}
}