using Domain.Entities.Enums;

namespace Application.Dtos.OrderResponse;

public record GetOrderResponse
{
	public int Id { get; init; }
	public OrderStatus Status { get; init; }
	public decimal Price { get; init; }
	public IEnumerable<GetOrderProductReponse> OrderProducts { get; init; } = Enumerable.Empty<GetOrderProductReponse>();
}
