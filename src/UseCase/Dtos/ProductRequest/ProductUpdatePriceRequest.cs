namespace UseCase.Dtos.ProductRequest;

public record ProductUpdatePriceRequest
{
	public decimal Price { get; set; }
}