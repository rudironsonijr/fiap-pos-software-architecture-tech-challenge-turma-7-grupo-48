using Domain.Entities.OrderAggregate;
using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.Extensions.OrderAggregate;

internal static class OrderExtension
{
	public static OrderSqlModel ToOrderSqlModel(this Order order)
	{
		var orderProducts = order.OrderProducts.Select(x => x.ToOrderProductSqlModel()).ToList();
		
		OrderSqlModel orderSqlModel = new()
		{

			CustomerId = order.CustomerId,
			Status = order.Status,
			PaymentProvider = order.PaymentMethod?.Provider,
			PaymentKind = order.PaymentMethod?.Kind,
			OrderProducts = orderProducts,
			Price = order.Price,
		};

		return orderSqlModel;
	}
}
