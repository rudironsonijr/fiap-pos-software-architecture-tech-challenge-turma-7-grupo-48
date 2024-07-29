using Domain.Entities.Enums;
using Domain.Entities.ProductAggregate;
using Domain.ValueObjects;
using UseCase.Dtos.ProductRequest;

namespace UseCase.Services.Interfaces;

public interface IProductUseCase
{
	Task<Product?> GetAsync(int id, CancellationToken cancellationToken);
	Task<Product?> CreateAsync(ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken);
	Task<IEnumerable<Product>> ListAsync(ProductType type, int? page, int? skip, CancellationToken cancellationToken);
	Task UpdatePriceAsync(int id, ProductUpdatePriceRequest productUpdatePrice, CancellationToken cancellationToken);
	Task UpdatePhoto(int id, Photo photo, CancellationToken cancellationToken);
}