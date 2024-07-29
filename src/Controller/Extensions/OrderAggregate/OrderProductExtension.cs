using Controller.Dtos.OrderResponse;
using Domain.Entities.OrderAggregate;

namespace Controller.Extensions.OrderAggregate;

internal static class OrderProductExtension
{
	public static UpdateOrderProductResponse ToUpdateOrderProductResponse(this OrderProduct product)
	{
		UpdateOrderProductResponse update = new()
		{
			Id = product.Id,
			ProductPrice = product.ProductPrice,
			Price = product.Price,
			Quantity = product.Quantity,
		};
		return update;
	}

	public static GetOrderProductReponse ToGetOrderProductReponse(this OrderProduct product)
	{
		GetOrderProductReponse getOrderProductReponse = new()
		{
			ProductId = product.ProductId,
			ProductPrice = product.ProductPrice,
			ProductName = product.Product.Name,
			Quantity = product.Quantity,
		};

		return getOrderProductReponse;
	}
}
