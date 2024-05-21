using Application.Dtos.PaymentRequest;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;

namespace Application.Services;

public class PaymentService : IPaymentService
{
	private readonly ICustomerRepository _customerRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IProductRepository _productRepository;
	private readonly NotificationContext _notificationContext;

	public PaymentService(
		IOrderRepository orderRepository,
		ICustomerRepository customerRepository,
		IProductRepository productRepository,
		NotificationContext notificationContext
	)
	{
		_orderRepository = orderRepository;
		_customerRepository = customerRepository;
		_productRepository = productRepository;
		_notificationContext = notificationContext;
	}

	public async Task<bool> CreatePaymentAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken)
	{
		Order orderResponse = await _orderRepository.GetAsync(createPaymentRequest.OrderId, cancellationToken);


		_notificationContext.AssertArgumentNotNull(orderResponse, $"Order with id:{createPaymentRequest.OrderId} not found");

		if (_notificationContext.HasErrors)
		{
			return false;
		}

		orderResponse.ChangeStatusToFinished();

		await _orderRepository.UpdateAsync(orderResponse, cancellationToken);

		return true;
	}
}