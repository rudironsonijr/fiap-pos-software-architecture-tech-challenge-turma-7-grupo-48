using Domain.Entities.Enums;

namespace Domain.Gateways
{
	public interface IPaymentGateway
	{
		Task<string> CreatePixPayment(int orderId, decimal price, PaymentProvider paymentProvider);
	}
}
