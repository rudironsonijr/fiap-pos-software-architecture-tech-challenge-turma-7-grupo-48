using Domain.Entities.CustomerAggregate;
using UseCase.Dtos.CustomerRequest;

namespace UseCase.Services.Interfaces;

public interface ICustomerUseCase
{
	Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken);
	Task<Customer?> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken);
}
