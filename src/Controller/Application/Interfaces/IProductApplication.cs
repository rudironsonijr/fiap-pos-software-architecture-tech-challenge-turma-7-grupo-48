using Controller.Dtos.ProductResponse;
using Domain.Entities.Enums;
using Domain.ValueObjects;
using UseCase.Dtos.ProductRequest;

namespace Controller.Application.Interfaces;

public interface IProductApplication
{
	Task<ProductGetResponse?> GetAsync(int id, CancellationToken cancellationToken);
	Task<Photo?> GetPhotoAsync(int id, CancellationToken cancellationToken);
	Task<ProductCreateResponse?> CreateAsync(ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken);
	Task<IEnumerable<ProductGetResponse>> ListAsync(ProductType type, int? page, int? skip, CancellationToken cancellationToken);
	Task UpdatePriceAsync(int id, ProductUpdatePriceRequest productUpdatePrice, CancellationToken cancellationToken);
	Task UpdatePhoto(int id, Photo photo, CancellationToken cancellationToken);
}
