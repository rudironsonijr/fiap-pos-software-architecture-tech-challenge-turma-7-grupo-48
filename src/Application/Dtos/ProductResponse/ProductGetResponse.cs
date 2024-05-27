using Domain.Entities.Enums;
using Domain.ValueObjects;

namespace Application.Dtos.ProductResponse;

public record ProductGetResponse
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public ProductType ProductType { get; init; }
	public decimal Price { get; init; }
}
