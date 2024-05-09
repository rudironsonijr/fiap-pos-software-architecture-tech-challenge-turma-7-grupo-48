namespace Application.Dtos.ProductRequest;

public record ProductUpdatePriceRequest
{
	public int Id { get; set; }
	public decimal Price { get; set; }
}