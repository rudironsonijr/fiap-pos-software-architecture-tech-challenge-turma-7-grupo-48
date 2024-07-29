using Controller.Application.Interfaces;
using Controller.Dtos.ProductResponse;
using Controller.Extensions.ProductAggregate;
using Domain.Entities.Enums;
using Domain.ValueObjects;
using UseCase.Dtos.ProductRequest;
using UseCase.Services.Interfaces;

namespace Controller.Application;

public class ProductApplication : IProductApplication
{
	public readonly IProductUseCase _productUseCase;
	public ProductApplication(IProductUseCase productUseCase)
	{
		_productUseCase = productUseCase;
	}
	public async Task<ProductGetResponse?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var product = await _productUseCase.GetAsync(id, cancellationToken);

		return
			product?.ToProductGetResponse();
	}

	public async Task<Photo?> GetPhotoAsync(int id, CancellationToken cancellationToken)
	{
		var product = await _productUseCase.GetAsync(id, cancellationToken);

		return
			product?.Photo;
	}

	public async Task<IEnumerable<ProductGetResponse>> ListAsync(ProductType type, int? page, int? skip, CancellationToken cancellationToken)
	{
		var products = await _productUseCase.ListAsync(type, page, skip, cancellationToken);

		return
			products.Select(x => x.ToProductGetResponse());
	}

	public async Task<ProductCreateResponse?> CreateAsync(ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken)
	{
		var product = await _productUseCase.CreateAsync(productCreateRequest, cancellationToken);

		return
			product?.ToProductCreateResponse();
	}

	public Task UpdatePriceAsync(int id, ProductUpdatePriceRequest productUpdatePrice, CancellationToken cancellationToken)
	{
		return
			_productUseCase.UpdatePriceAsync(id, productUpdatePrice, cancellationToken);
	}

	public Task UpdatePhoto(int id, Photo photo, CancellationToken cancellationToken)
	{
		return
			_productUseCase.UpdatePhoto(id, photo, cancellationToken);
	}
}
