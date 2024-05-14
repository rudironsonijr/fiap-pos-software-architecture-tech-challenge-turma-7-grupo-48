using Application.Dtos.ProductRequest;
using Application.Dtos.ProductResponse;
using Domain.ValueObjects;

namespace Application.Services.Interfaces;

public interface IProductService {

	Task<ProductCreateResponse?> CreateAsync(ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken);

	Task AddPhoto(int id, Photo photo, CancellationToken cancellationToken);
}