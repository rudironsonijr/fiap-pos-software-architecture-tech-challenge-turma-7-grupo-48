using Domain.Entities.Enums;
using Helpers;
using Integration.Strategies.Pix.Interface;

namespace Integration.Strategies.Pix;

public class PixStrategyResolver : IPixStrategyResolver
{
	private readonly IEnumerable<IPixPaymentStrategy> _pixPaymentStrategies;
	public PixStrategyResolver(IEnumerable<IPixPaymentStrategy> pixPaymentStrategies)
	{
		_pixPaymentStrategies = pixPaymentStrategies;
	}

	public IPixPaymentStrategy Resolve(PaymentProvider paymentProvider)
	{
		var response = _pixPaymentStrategies.FirstOrDefault(x => x.paymentProvider == paymentProvider);

		if (response == null)
		{
			throw new InvalidOperationException($"Pix Strategy not implemente for: {paymentProvider.GetEnumDescription()}");
		}

		return response;
	}
}
