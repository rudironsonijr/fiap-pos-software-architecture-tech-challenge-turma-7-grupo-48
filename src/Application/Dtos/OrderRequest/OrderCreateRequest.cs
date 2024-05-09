namespace Application.Dtos.OrderRequest;

public record OrderCreateRequest
{
	public string? CustomerId { get; set; }
	public OrderAddProductRequest Product { get; set; } = new();
}