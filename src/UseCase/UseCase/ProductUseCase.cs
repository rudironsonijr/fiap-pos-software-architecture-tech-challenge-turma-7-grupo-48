using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Entities.ProductAggregate;
using Domain.Repositories;
using Domain.ValueObjects;
using UseCase.Dtos.ProductRequest;
using UseCase.Dtos.ProductRequest.Extensions;
using UseCase.Services.Interfaces;

namespace UseCase.Services;

public class ProductUseCase : IProductUseCase
{
	private readonly NotificationContext _notificationContext;
	public IProductRepository _productRepository { get; set; }
	public ProductUseCase(IProductRepository productRepository, NotificationContext notificationContext)
	{
		_productRepository = productRepository;
		_notificationContext = notificationContext;
	}

	public Task<Product?> GetAsync(int id, CancellationToken cancellationToken)
	{
		return _productRepository.GetAsync(id, cancellationToken);
	}

	public Task<IEnumerable<Product>> ListAsync(ProductType type, int? page, int? skip, CancellationToken cancellationToken)
	{
		return _productRepository.ListAsync(type, page, skip, cancellationToken);

	}

	public async Task<Product?> CreateAsync(ProductCreateRequest productCreateRequest,
		CancellationToken cancellationToken)
	{

		_notificationContext
			.AssertArgumentNotNullOrWhiteSpace(productCreateRequest.Name, $"The field name is required")
			.AssertArgumentNotNullOrWhiteSpace(productCreateRequest.Description, $"The field Description is required")
			.AssertArgumentIsMinimumLengthOrLess(productCreateRequest.Price, 0, "The product price can't be 0");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		var product = productCreateRequest.ToProduct();

		product = await _productRepository.CreateAsync(product, cancellationToken);

		return product;
	}

	public async Task UpdatePriceAsync(int id, ProductUpdatePriceRequest productUpdatePrice, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetAsync(id, cancellationToken);

		_notificationContext
			.AssertArgumentNotNull(product, $"Product with id:{id} not found")
			.AssertArgumentIsMinimumLengthOrLess(productUpdatePrice.Price, 0, "The product price can't be 0");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		product!.Price = productUpdatePrice.Price;

		await _productRepository.UpdateAsync(product, cancellationToken);
	}

	public async Task UpdatePhoto(int id, Photo photo, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetAsync(id, cancellationToken);

		_notificationContext.AssertArgumentNotNull(product, $"Product with id:{id} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		product!.Photo = photo;

		await _productRepository.UpdateAsync(product, cancellationToken);
	}
}