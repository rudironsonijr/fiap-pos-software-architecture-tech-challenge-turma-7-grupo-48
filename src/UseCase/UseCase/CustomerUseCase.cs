using Core.Notifications;
using Domain.Entities.CustomerAggregate;
using Domain.Repositories;
using UseCase.Dtos.CustomerRequest;
using UseCase.Services.Interfaces;

namespace UseCase.Services;

public class CustomerUseCase : ICustomerUseCase
{
	private readonly ICustomerRepository _customerRepository;
	private readonly NotificationContext _notificationContext;
	public CustomerUseCase(
		ICustomerRepository customerRepository,
		NotificationContext notificationContext)
	{
		_customerRepository = customerRepository;
		_notificationContext = notificationContext;
	}

	public async Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken)
	{
		_notificationContext.AssertArgumentInvalidCpf(cpf);

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		var customer = await _customerRepository.GetByCpf(cpf, cancellationToken);

		return
			customer;
	}

	public async Task<Customer?> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken)
	{
		_notificationContext
			.AssertArgumentNotNullOrWhiteSpace(createCustomerRequest.Name, $"The field name is required")
			.AssertArgumentInvalidCpf(createCustomerRequest.Cpf)
			.AssertArgumentInvalidEmail(createCustomerRequest.Email);

		var cpfAlreadyExist = await _customerRepository.ExistsByCpf(createCustomerRequest.Cpf, cancellationToken);

		if (cpfAlreadyExist)
		{
			_notificationContext.AddNotification($"CPF {createCustomerRequest.Cpf} already exists!");
		}


		if (_notificationContext.HasErrors)
		{
			return null;
		}

		Customer customer = new()
		{
			Name = createCustomerRequest.Name,
			Email = createCustomerRequest.Email,
			Cpf = createCustomerRequest.Cpf,
		};

		customer = await _customerRepository.CreateAsync(customer, cancellationToken);

		return customer;
	}
}
