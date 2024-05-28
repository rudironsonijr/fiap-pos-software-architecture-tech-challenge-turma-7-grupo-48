using Domain.Entities.ProductAggregate;
using Infrastructure.SqlModels.ProductAggregate;

namespace Infrastructure.Extensions.ProductAggregate;

internal static class ProductExtension
{
	public static ProductSqlModel ToProductSqlModel(this Product product)
	{
		var response = new ProductSqlModel()
		{
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			ProductType = product.ProductType,
			PhotoContentType = product.Photo?.ContentType,
			PhotoFilename = product.Photo?.ContentType,
			PhotoData = product.Photo?.Data,
		};

		return response;
	}
}
