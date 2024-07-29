using Domain.Entities.PaymentAggregate;
using Infrastructure.SqlModels.PaymentAggregate;

namespace Infrastructure.Extensions.PaymentAggregate;

internal static class PaymentExtension
{
	public static PaymentSqlModel ToPaymentSqlModel(this Payment payment)
	{
		return new()
		{
			Id = payment.Id,
			Status = payment.Status,
			PaymentProvider = payment.PaymentMethod.Provider,
			PaymentKind = payment.PaymentMethod.Kind,
			OrderId = payment.OrderId,
			ExternalId = payment.ExternalId,
			Amount = payment.Amount,
		};
	}
}
