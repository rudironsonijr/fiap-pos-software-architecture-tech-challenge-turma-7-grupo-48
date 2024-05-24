using Domain.Entities.ProductAggregate;
using Domain.ValueObjects;

namespace Infrastructure.SqlModels.ProductAggregate.Extensions;

internal static class ProductSqlModelExtension
{
	public static Product ToProduct(this ProductSqlModel productSqlModel)
	{
		Photo? photo = null;
		if (productSqlModel.PhotoData != null)
		{
			photo = new Photo(productSqlModel.PhotoFilename, productSqlModel.PhotoContentType, productSqlModel.PhotoData);
		}

		return new()
		{
			Id = productSqlModel.Id,
			Name = productSqlModel.Name,
			Description = productSqlModel.Description,
			ProductType = productSqlModel.ProductType,
			Price = productSqlModel.Price,
			Photo = photo
		};
	}
}
