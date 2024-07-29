using Domain.Entities.OrderAggregate;
using Domain.Gateways.Dtos.PaymentGateway;

namespace Domain.Gateways;

public interface IPaymentGateway
{
	Task<CreatePixResponse> CreatePixPayment(Order order);
}
