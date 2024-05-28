using Domain.Entities.OrderAggregate;
using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.Extensions.OrderAggregate;

internal static class OrderProductExtension
{
	public static OrderProductSqlModel ToOrderProductSqlModel(this OrderProduct orderProduct)
	{
		OrderProductSqlModel response = new()
		{
			Id = orderProduct.Id,
			ProductId = orderProduct.ProductId,
			OrderId = orderProduct.OrderId,
			ProductPrice = orderProduct.ProductPrice,
			Quantity = orderProduct.Quantity,
		};
		return response;
	}
}
