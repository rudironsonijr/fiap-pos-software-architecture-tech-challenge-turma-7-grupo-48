using Domain.ValueObjects;

namespace Domain.Entities.ProductAggregate;

public class ProductPhoto
{
	public int Id { get; init; }
	public required Photo Photo { get; init; }
}
