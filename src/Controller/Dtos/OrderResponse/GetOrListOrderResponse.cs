using Domain.Entities.Enums;

namespace Controller.Dtos.OrderResponse;

public record GetOrListOrderResponse
{
	public int Id { get; init; }
	public OrderStatus Status { get; init; }
	public decimal Price { get; init; }
	public IEnumerable<GetOrderProductReponse> OrderProducts { get; init; } = Enumerable.Empty<GetOrderProductReponse>();
}
