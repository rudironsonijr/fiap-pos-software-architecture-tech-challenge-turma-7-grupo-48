using Controller.Dtos.CustomerResponse;
using UseCase.Dtos.CustomerRequest;

namespace Controller.Application.NewFolder;

public interface ICustomerApplication
{
	Task<GetCustomerResponse?> GetByCpf(string cpf, CancellationToken cancellationToken);
	Task<CreateCustomerResponse?> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken);
}
