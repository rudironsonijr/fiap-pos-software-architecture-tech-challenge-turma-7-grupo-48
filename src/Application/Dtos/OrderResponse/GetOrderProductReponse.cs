namespace Application.Dtos.OrderResponse;

public record GetOrderProductReponse
{
	public int Id { get; init; }
	public int ProductId { get; init; }
	public string ProductName { get; init; } = string.Empty;
	public decimal ProductPrice { get; init; }
	public decimal Price { get; init; }
	public int Quantity { get; set; }

}
