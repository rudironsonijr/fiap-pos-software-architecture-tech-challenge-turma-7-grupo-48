namespace Application.Dtos.OrderRequest;

public record CreateOrderRequest
{
	public int? CustomerId { get; set; }
	public OrderAddProductRequest Product { get; set; } = new();
}