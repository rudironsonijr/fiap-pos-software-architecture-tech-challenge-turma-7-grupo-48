using Application.Dtos.OrderResponse;
using Domain.Entities.OrderAggregate;

namespace Application.Extensions.OrderAggregate;

internal static class OrderExtension
{
	public static OrderUpdateOrderProductResponse ToOrderUpdateProductResponse(this Order order)
	{
		List<UpdateOrderProductResponse> orderProductsResponse = [];

		foreach(var orderProduct in order.Products) {
			var updateOrderProduct = orderProduct.ToUpdateOrderProductResponse();
			orderProductsResponse.Add(updateOrderProduct);
		}

		OrderUpdateOrderProductResponse response = new()
		{
			OrderId = order.Id,
			Price = order.Price,
			OrderProduct = orderProductsResponse
		};

		return response;

	}
}
