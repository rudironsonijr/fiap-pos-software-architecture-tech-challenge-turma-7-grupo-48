using Application.Dtos.ProductResponse;
using Domain.Entities.ProductAggregate;

namespace Application.Extensions.ProductAggregate;

internal static class ProductExtension
{
	public static ProductGetResponse ToProductGetResponse(this Product product)
	{
		return new() 
		{ 
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			ProductType = product.ProductType,
			Price = product.Price,
		};
	}
}
