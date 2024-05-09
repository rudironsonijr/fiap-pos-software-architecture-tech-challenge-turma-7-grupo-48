namespace Application.Dtos.OrderRequest;

public record CreateOrderRequest
{
	public string? CustomerId { get; set; }
	public OrderAddProductRequest Product { get; set; } = new();
}