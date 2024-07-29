using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Gateways.Dtos.PaymentGateway;
using Domain.ValueObjects;

namespace Integration.Strategies.Pix.Interface;

public interface IPixPaymentStrategy
{
	PaymentProvider paymentProvider { get; }

	Task<CreatePixResponse> CreatePayment(Order order);
}
