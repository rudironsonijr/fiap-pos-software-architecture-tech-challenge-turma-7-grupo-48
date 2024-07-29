using Controller.Application.Interfaces;
using Controller.Dtos.CustomerResponse;
using Controller.Extensions.CustomerAggregate;
using UseCase.Dtos.CustomerRequest;
using UseCase.Services.Interfaces;

namespace Controller.Application;

public class CustomerApplication : ICustomerApplication
{
	private readonly ICustomerUseCase _customerUseCase;
	public CustomerApplication(ICustomerUseCase customerUseCase)
	{
		_customerUseCase = customerUseCase;
	}

	public async Task<CreateCustomerResponse?> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken)
	{

		var customer = await _customerUseCase.CreateAsync(createCustomerRequest, cancellationToken);
		return customer?.ToCreateCustomerResponse(); 
	}

	public async Task<GetCustomerResponse?> GetByCpf(string cpf, CancellationToken cancellationToken)
	{
		var customer = await _customerUseCase.GetByCpf(cpf, cancellationToken);

		return
			customer?.ToGetCustomerResponse();
	}
}
