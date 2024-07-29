using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Entities.PaymentAggregate;
using Domain.Gateways;
using Domain.Repositories;
using Domain.ValueObjects;
using Helpers;
using UseCase.Services.Interfaces;

namespace UseCase.Services;

public class PaymentUseCase : IPaymentUseCase
{
	private readonly IOrderRepository _orderRepository;
	private readonly IPaymentRepository _paymentRepository;
	private readonly IPaymentGateway _paymentGateway;
	private readonly NotificationContext _notificationContext;

	public PaymentUseCase(
		IOrderRepository orderRepository,
		IPaymentRepository paymentRepository,
		IPaymentGateway paymentGateway,
		NotificationContext notificationContext
	)
	{
		_orderRepository = orderRepository;
		_paymentRepository = paymentRepository;
		_paymentGateway = paymentGateway;
		_notificationContext = notificationContext;
	}

	public async Task<Photo?> CreatePixAsync(int orderId, PaymentProvider provider, CancellationToken cancellationToken)
	{
		Order? order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		_notificationContext.AssertArgumentEnumInvalidValue(provider, PaymentProvider.None,
			$"Payment Method Can't be {PaymentProvider.None.GetEnumDescription()}");

		ValidateOrderStatusToPayment(order);

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		PaymentMethod paymentMethod = new(provider, PaymentMethodKind.Pix);

		order!.PaymentMethod = paymentMethod;

		var createPixResponse = await _paymentGateway.CreatePixPayment(order);

		_notificationContext.AssertArgumentEnumInvalidValue(provider, PaymentStatus.None,
			$"Payment Method Can't be {PaymentStatus.None.GetEnumDescription()}");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		var payment = new Payment
		{
			OrderId = orderId,
			Amount = order.Price,
			PaymentMethod = paymentMethod,
			ExternalId = createPixResponse.ExternalId,
			Status = createPixResponse.Status
		};

		await _orderRepository.UpdateAsync(order, cancellationToken);
		await _paymentRepository.CreateAsync(payment, cancellationToken);

		return createPixResponse.QrCode;
	}

	public async Task ConfirmPaymentAsync(string externalId, CancellationToken cancellationToken)
	{

		var payment = await _paymentRepository.GetByExternalIdAsync(externalId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(payment, $"Payment with external id:{externalId} not found");
		if (_notificationContext.HasErrors)
		{
			return;
		}

		var orderId = payment!.OrderId;
		Order? orderResponse = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(orderResponse, $"Order with id:{orderId} not found");
		_notificationContext.AssertArgumentNotNull(
			orderResponse?.PaymentMethod, $"Order with id:{orderId} has no defined Payment Method");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		orderResponse!.ChangeStatusToReceived();
		payment.Status = PaymentStatus.Paid;

		await _orderRepository.UpdateAsync(orderResponse, cancellationToken);
		await _paymentRepository.UpdateAsync(payment, cancellationToken);

	}

	private void ValidateOrderStatusToPayment(Order? order)
	{
		if (order == null)
		{
			return;
		}

		_notificationContext.AssertArgumentEnumInvalidValue(order.Status, OrderStatus.Creating,
			$"Set Payment its only possible in status {OrderStatus.Creating.GetEnumDescription()}");
	}
}