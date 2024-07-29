using Domain.Entities.OrderAggregate;
using Domain.Gateways;
using Domain.Gateways.Dtos.PaymentGateway;
using Integration.Strategies.Pix.Interface;

namespace Integration.Gateway;
public class PaymentGateway : IPaymentGateway
{
	private readonly IPixStrategyResolver _pixStrategyResolver;
	public PaymentGateway(IPixStrategyResolver pixStrategyResolver)
	{
		_pixStrategyResolver = pixStrategyResolver;
	}
	public Task<CreatePixResponse> CreatePixPayment(Order order)
	{
		if (order.PaymentMethod == null)
		{
			throw new ArgumentException("Order Payment Method Can't be null");
		}

		return
			_pixStrategyResolver.Resolve(order.PaymentMethod.Value.Provider).CreatePayment(order);
	}
}
