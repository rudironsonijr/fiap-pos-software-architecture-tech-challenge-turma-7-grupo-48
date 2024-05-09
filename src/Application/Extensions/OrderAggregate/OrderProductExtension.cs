using Application.Dtos.OrderResponse;
using Domain.Entities.OrderAggregate;

namespace Application.Extensions.OrderAggregate;

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
}
