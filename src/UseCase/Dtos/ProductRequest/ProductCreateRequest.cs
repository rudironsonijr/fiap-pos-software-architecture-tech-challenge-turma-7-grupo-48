using Domain.Entities.Enums;
using Domain.ValueObjects;

namespace UseCase.Dtos.ProductRequest;

public record ProductCreateRequest
{
	public string Name { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public decimal Price { get; init; }
	public ProductType ProductType { get; init; }
	public Photo? photo { get; init; }

}