using Domain.Entities.ProductAggregate;

namespace UseCase.Dtos.ProductRequest.Extensions;

public static class ProductCreateRequestExtension
{
	public static Product ToProduct(this ProductCreateRequest productCreateRequest)
	{
		Product product = new()
		{
			Description = productCreateRequest.Description,
			Name = productCreateRequest.Name,
			ProductType = productCreateRequest.ProductType,
			Price = productCreateRequest.Price
		};
		return product;
	}
}