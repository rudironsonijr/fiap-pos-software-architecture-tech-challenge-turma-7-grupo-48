using Domain.Entities.OrderAggregate;
using Domain.ValueObjects;

namespace Infrastructure.SqlModels.OrderAggregate.Extensions;

internal static class OrderSqlModelExtension
{
	public static Order ToOrder(this OrderSqlModel orderSqlModel)
	{
		var orderProducts = orderSqlModel.OrderProducts.Select(x => x.ToOrderProduct()).ToList();

		PaymentMethod? paymentMethod = null;
		if (orderSqlModel.PaymentProvider != null && orderSqlModel.PaymentKind != null)
		{
			paymentMethod = new PaymentMethod(orderSqlModel.PaymentProvider.Value, orderSqlModel.PaymentKind.Value);
		}

		Order order = new(orderSqlModel.Id, orderSqlModel.Status, orderProducts, paymentMethod, orderSqlModel.CreatedAt)
		{
			CustomerId = orderSqlModel.CustomerId,
		};

		return order;
	}
}
