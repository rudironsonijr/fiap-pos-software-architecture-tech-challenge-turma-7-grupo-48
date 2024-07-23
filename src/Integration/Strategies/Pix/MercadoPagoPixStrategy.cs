using Domain.Entities.Enums;
using Integration.Strategies.Pix.Interface;

namespace Integration.Strategies.Pix;

public class MercadoPagoPixStrategy : IPixPaymentStrategy
{
	public PaymentProvider paymentProvider => PaymentProvider.MercadoPago;

	public Task<string> CreatePayment(int id, decimal value)
	{
		throw new NotImplementedException();
	}
}
