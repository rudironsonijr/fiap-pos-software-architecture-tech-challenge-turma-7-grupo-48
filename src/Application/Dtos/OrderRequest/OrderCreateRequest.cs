namespace Application.Dtos.OrderRequest;

public record OrderCreateRequest
{
	public string? CustomerCpf { get; set; }
	public OrderAddProductRequest Product { get; set; } = new();
}