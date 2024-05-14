using Domain.Entities.CustomerAggregate;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
	Task<Customer?> GetByCpf(
		string cpf,
		CancellationToken cancellationToken);

	Task<Customer?> GetAsync(
		string id,
		CancellationToken cancellationToken);

	Task<Customer> CreateAsync(
		Customer id,
		CancellationToken cancellationToken);

	Task<bool> ExistsByCpf(
		string? cpf,
		CancellationToken cancellationToken);

}