namespace UseCase.Dtos.OrderRequest;

public record OrderUpdateProductQuantityRequest
{
	public int Quantity { get; set; }
}