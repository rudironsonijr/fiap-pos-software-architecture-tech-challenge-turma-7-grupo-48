using Domain.Entities.Enums;
using Domain.Gateways;
using Integration.Strategies.Pix.Interface;

namespace Integration.Gateway;
public class PaymentGateway : IPaymentGateway
{
	private readonly IPixStrategyResolver _pixStrategyResolver;
	public PaymentGateway(IPixStrategyResolver pixStrategyResolver)
	{
		_pixStrategyResolver = pixStrategyResolver;
	}
	public Task<string> CreatePixPayment(int orderId, decimal price, PaymentProvider paymentProvider)
	{
		return
			_pixStrategyResolver.Resolve(paymentProvider).CreatePayment(orderId, price);
	}
}
