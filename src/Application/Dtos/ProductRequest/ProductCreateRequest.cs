using Domain.Entities.Enums;

namespace Application.Dtos.ProductRequest;

public record ProductCreateRequest
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public decimal Price { get; init; }
	public ProductType ProductType { get; init; }
}
