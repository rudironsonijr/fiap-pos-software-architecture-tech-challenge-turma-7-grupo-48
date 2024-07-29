using Domain.Entities.PaymentAggregate;
using Domain.ValueObjects;

namespace Infrastructure.SqlModels.PaymentAggregate.Extensions;

internal static class PaymentSqlModelExtensions
{
	public static Payment ToPayment(this PaymentSqlModel payment)
	{
		return new()
		{
			Id = payment.Id,
			Status = payment.Status,
			PaymentMethod = new PaymentMethod(payment.PaymentProvider, payment.PaymentKind),
			OrderId = payment.OrderId,
			ExternalId = payment.ExternalId,
			Amount = payment.Amount,
		};
	}
}
