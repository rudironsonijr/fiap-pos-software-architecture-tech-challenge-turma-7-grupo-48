using UseCase.Dtos.PaymentRequest;
using UseCase.Dtos.PaymentResponse;
using UseCase.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Domain.ValueObjects;
using Helpers;
using Domain.Gateways;

namespace UseCase.Services;

public class PaymentUseCase : IPaymentUseCase
{
	private readonly IOrderRepository _orderRepository;
	private readonly IPaymentGateway _paymentGateway;
	private readonly NotificationContext _notificationContext;

	public PaymentUseCase(
		IOrderRepository orderRepository,
		IPaymentGateway paymentGateway,
		NotificationContext notificationContext
	)
	{
		_orderRepository = orderRepository;
		_paymentGateway = paymentGateway;
		_notificationContext = notificationContext;
	}

	public async Task<string?> CreatePixAsync(int orderId, PaymentProvider provider, CancellationToken cancellationToken)
	{
		Order? order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		_notificationContext.AssertArgumentEnumInvalidValue(provider, PaymentProvider.None, 
			$"Payment Method Can't be {PaymentProvider.None.GetEnumDescription()}");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		PaymentMethod paymentMethod = new(provider, PaymentMethodKind.Pix);

		order!.PaymentMethod = paymentMethod;

		var pix = await _paymentGateway.CreatePixPayment(order.Id, order.Price, provider);

		await _orderRepository.UpdateAsync(order, cancellationToken);
		
		return pix;
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