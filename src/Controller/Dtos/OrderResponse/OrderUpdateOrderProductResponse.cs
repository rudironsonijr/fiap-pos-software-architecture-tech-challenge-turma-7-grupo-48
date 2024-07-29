namespace Controller.Dtos.OrderResponse;

public record OrderUpdateOrderProductResponse
{
	public int OrderId { get; init; }
	public decimal Price { get; init; }
	public IEnumerable<UpdateOrderProductResponse> OrderProducts { get; init; } = [];
}
