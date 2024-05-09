using Domain.Entities.CustomerAggregate;

namespace Domain.Repositories;

public interface ICustomerRepository
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
}