using Application.Dtos.ProductRequest;
using Application.Dtos.ProductRequest.Extensions;
using Application.Dtos.ProductResponse;
using Application.Services.Interfaces;
using Domain.Repositories;

namespace Application.Services;

public class ProductService : IProductService
{
	public ProductService(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public IProductRepository _productRepository { get; set; }

	public async Task<ProductCreateResponse> CreateAsync(
		ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken
	)
	{
		//ToDo: Implementar validações
		var product = productCreateRequest.ToProduct();
		product = await _productRepository.CreateAsync(product, cancellationToken);
		ProductCreateResponse response = new()
		{
			Id = product.Id
		};
		return response;
	}

	public async Task UpdatePriceAsync(
		ProductUpdatePriceRequest productUpdatePrice,
		CancellationToken cancellationToken
	)
	{
		//ToDo: Implementar validações
		var product = await _productRepository.GetAsync(productUpdatePrice.Id, cancellationToken);
		product.Price = productUpdatePrice.Price;
		await _productRepository.UpdateAsync(product, cancellationToken);
	}
}