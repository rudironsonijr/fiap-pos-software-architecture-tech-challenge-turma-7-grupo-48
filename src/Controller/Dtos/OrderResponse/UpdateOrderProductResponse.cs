namespace Controller.Dtos.OrderResponse;

public record UpdateOrderProductResponse
{
	public int Id { get; init; }
	public decimal ProductPrice { get; init; }
	public decimal Price { get; init; }
	public int Quantity { get; init; }
}
