using UseCase.Dtos.ProductRequest;
using Domain.ValueObjects;
using WebApi.Extensions;

namespace WebApi.DTOs.Extensions;

internal static class ProductCreateWithImageRequestExtension
{
	public static async Task<ProductCreateRequest> ToProductCreateRequestAsync(
		this ProductCreateWithFormFileRequest productCreateRequestWithImage, CancellationToken cancellationToken)
	{
		Photo? photo = null;
		if(productCreateRequestWithImage.Photo != null)
		{
			photo = await productCreateRequestWithImage.Photo.ToPhotoAsync(cancellationToken);
		}

		return new ProductCreateRequest()
		{
			Name = productCreateRequestWithImage.Name,
			Price = productCreateRequestWithImage.Price,
			ProductType = productCreateRequestWithImage.ProductType,
			Description = productCreateRequestWithImage.Description,
			photo = photo
		};
	}
}
