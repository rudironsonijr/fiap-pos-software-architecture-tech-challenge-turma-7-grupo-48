namespace UseCase.Dtos.OrderRequest;

public record OrderAddProductRequest
{
	public int ProductId { get; set; }
	public int Quantity { get; set; }
}