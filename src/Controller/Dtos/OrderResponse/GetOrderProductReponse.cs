namespace Controller.Dtos.OrderResponse;

public record GetOrderProductReponse
{
	public int ProductId { get; init; }
	public string ProductName { get; init; } = string.Empty;
	public decimal ProductPrice { get; init; }
	public int Quantity { get; set; }

}
