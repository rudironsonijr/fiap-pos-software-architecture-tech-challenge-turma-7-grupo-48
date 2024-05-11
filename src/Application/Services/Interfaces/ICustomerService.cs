using Application.Dtos.CustomerRequest;
using Application.Dtos.CustomerResponse;

namespace Application.Services.Interfaces;

public interface ICustomerService
{
	Task<GetCustomerResponse?> GetByCpf(string cpf, CancellationToken cancellationToken);
	Task<CreateCustomerResponse?> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken);
}
