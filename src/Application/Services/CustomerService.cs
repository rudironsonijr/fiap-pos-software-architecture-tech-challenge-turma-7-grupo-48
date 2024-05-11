using Application.Dtos.CustomerRequest;
using Application.Dtos.CustomerResponse;
using Application.Extensions.CustomerAggregate;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.CustomerAggregate;
using Domain.Repositories;

namespace Application.Services;

public class CustomerService : ICustomerService
{
	private readonly ICustomerRepository _customerRepository;
	private readonly NotificationContext _notificationContext;
	public CustomerService(
		ICustomerRepository customerRepository, 
		NotificationContext notificationContext)
	{
		_customerRepository = customerRepository;
		_notificationContext = notificationContext;
	}

	public async Task<GetCustomerResponse?> GetByCpf(string cpf, CancellationToken cancellationToken)
	{
		_notificationContext.AssertArgumentInvalidCpf(cpf);

		if(_notificationContext.HasErrors)
		{
			return null;
		}

		var customer = await _customerRepository.GetByCpf(cpf, cancellationToken);

		return 
			customer?.ToGetCustomerResponse();
	}

	public async Task<CreateCustomerResponse?> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken)
	{
		_notificationContext.AssertArgumentNotNullOrWhiteSpace(createCustomerRequest.Name, $"The field name is required");
		_notificationContext.AssertArgumentInvalidCpf(createCustomerRequest.Cpf);
		_notificationContext.AssertArgumentInvalidEmail(createCustomerRequest.Email);

		var cpfAlreadyExist = await _customerRepository.ExistsByCpf(createCustomerRequest.Cpf, cancellationToken);
		if (cpfAlreadyExist)
		{
			_notificationContext.AddNotification($"CPF {createCustomerRequest.Cpf} already exists!");
		}

	
		if(_notificationContext.HasErrors)
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

		CreateCustomerResponse response = new()
		{
			CustomerId = customer.Id,
		};
		
		return response;
	}
}
