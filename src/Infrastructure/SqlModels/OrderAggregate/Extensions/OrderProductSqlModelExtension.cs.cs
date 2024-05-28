using Domain.Entities.OrderAggregate;
using Infrastructure.SqlModels.ProductAggregate.Extensions;

namespace Infrastructure.SqlModels.OrderAggregate.Extensions;

internal static class OrderProductSqlModelExtension
{
	public static OrderProduct ToOrderProduct(this OrderProductSqlModel orderProductSqlModel)
	{
		var product = orderProductSqlModel.Product?.ToProduct();
		OrderProduct response = new(product!)
		{
			Id = orderProductSqlModel.Id,
			OrderId = orderProductSqlModel.OrderId,
			ProductPrice = orderProductSqlModel.ProductPrice,
			Quantity = orderProductSqlModel.Quantity,
		};
		return response;
	}
}
