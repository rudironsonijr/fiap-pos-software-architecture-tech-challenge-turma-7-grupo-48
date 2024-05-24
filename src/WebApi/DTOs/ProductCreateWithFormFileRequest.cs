using Domain.Entities.Enums;

namespace WebApi.DTOs;

public class ProductCreateWithFormFileRequest
{
	public string Name { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public decimal Price { get; init; }
	public ProductType ProductType { get; init; }
	public IFormFile? Photo { get; init; }
}
