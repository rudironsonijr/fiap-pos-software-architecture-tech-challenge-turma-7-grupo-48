using Application.Dtos.PaymentRequest;
using Application.Dtos.PaymentResponse;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Domain.ValueObjects;
using Helpers;

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

	public async Task<CreatePaymentResponse?> CreateAsync(CreatePaymentRequest createPayment, CancellationToken cancellationToken)
	{
		Order? orderResponse = await _orderRepository.GetAsync(createPayment.OrderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(orderResponse, $"Order with id:{createPayment.OrderId} not found");

		_notificationContext.AssertArgumentEnumInvalidValue(createPayment.Provider, PaymentProvider.None, 
			$"Payment Method Can't be {PaymentProvider.None.GetEnumDescription()}");

		_notificationContext.AssertArgumentEnumInvalidValue(createPayment.Kind, PaymentMethodKind.None,
			$"Payment Kind Can't be {PaymentMethodKind.None.GetEnumDescription()}");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		PaymentMethod paymentMethod = new(createPayment.Provider, createPayment.Kind);

		orderResponse!.PaymentMethod = paymentMethod;

		await _orderRepository.UpdateAsync(orderResponse, cancellationToken);

		CreatePaymentResponse response = new()
		{
			PaymentId = Guid.NewGuid().ToString(),
		};
		
		return response;
	}

	public async Task ConfirmPaymentAsync(int orderId, CancellationToken cancellationToken)
	{
		Order? orderResponse = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(orderResponse, $"Order with id:{orderId} not found");
		_notificationContext.AssertArgumentNotNull(orderResponse?.PaymentMethod, $"Order with id:{orderId} has no defined Payment Method");

		if (_notificationContext.HasErrors)
		{
			return;
		}


		orderResponse!.ChangeStatusToReceived();

		await _orderRepository.UpdateAsync(orderResponse, cancellationToken);

		return;
	}
}