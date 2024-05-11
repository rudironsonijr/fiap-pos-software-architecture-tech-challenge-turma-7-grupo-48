namespace Application.Dtos.OrderRequest;

public record OrderUpdateProductQuantityRequest
{
	public int Quantity { get; set; }
}