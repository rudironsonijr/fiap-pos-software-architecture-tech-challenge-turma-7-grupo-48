namespace Application.Dtos.ProductRequest;

public record ProductUpdatePriceRequest
{
	public decimal Price { get; set; }
}