using Application.Dtos.ProductRequest;
using Application.Dtos.ProductResponse;
using Domain.Entities.Enums;
using Domain.ValueObjects;

namespace Application.Services.Interfaces;

public interface IProductService 
{
	Task<ProductGetResponse?> GetAsync(int id, CancellationToken cancellationToken);
	Task<ProductCreateResponse?> CreateAsync(ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken);
	Task<IEnumerable<ProductGetResponse>> ListAsync(ProductType type, int? page, int? skip, CancellationToken cancellationToken);
	Task UpdatePriceAsync(int id, ProductUpdatePriceRequest productUpdatePrice, CancellationToken cancellationToken);
	Task UpdatePhoto(int id, Photo photo, CancellationToken cancellationToken);
}