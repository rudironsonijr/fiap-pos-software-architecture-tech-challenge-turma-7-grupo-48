using Domain.Entities.CustomerAggregate;
using Domain.Repositories.Base;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
	Task<Customer?> GetByCpf(
		Cpf cpf,
		CancellationToken cancellationToken);

	Task<Customer?> GetAsync(
		string id,
		CancellationToken cancellationToken);

	Task<int> CreateAsync(
		Customer id,
		CancellationToken cancellationToken);

	Task<bool> ExistsByCpf(
		Cpf cpf,
		CancellationToken cancellationToken);

}