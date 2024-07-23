using Domain.Entities.Enums;

namespace Integration.Strategies.Pix.Interface;

public interface IPixPaymentStrategy
{
	PaymentProvider paymentProvider { get; }

	Task<string> CreatePayment(int id, decimal value);
}
