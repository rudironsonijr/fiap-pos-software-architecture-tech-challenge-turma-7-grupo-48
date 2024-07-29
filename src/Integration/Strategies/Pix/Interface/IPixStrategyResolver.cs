using Domain.Entities.Enums;

namespace Integration.Strategies.Pix.Interface;

public interface IPixStrategyResolver
{
	IPixPaymentStrategy Resolve(PaymentProvider paymentProvider);
}
