using Application.Dtos.ProductRequest;
using Application.Dtos.ProductRequest.Extensions;
using Application.Dtos.ProductResponse;
using Application.Extensions.ProductAggregate;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Services;

public class ProductService : IProductService
{
	private readonly NotificationContext _notificationContext;
	public IProductRepository _productRepository { get; set; }
	public ProductService(IProductRepository productRepository, NotificationContext notificationContext)
	{
		_productRepository = productRepository;
		_notificationContext = notificationContext;
	}

	public async Task<ProductGetResponse?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetAsync(id, cancellationToken);

		return
			product?.ToProductGetResponse();
	}

	public async Task<IEnumerable<ProductGetResponse>> ListAsync(ProductType type, int? page, int? skip, CancellationToken cancellationToken)
	{
		var products = await _productRepository.ListAsync(type, page, skip, cancellationToken);

		return
			products.Select(x => x.ToProductGetResponse());
	}

	public async Task<ProductCreateResponse?> CreateAsync(ProductCreateRequest productCreateRequest,
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

		var productId = await _productRepository.CreateAsync(product, cancellationToken);

		ProductCreateResponse response = new()
		{
			Id = productId
		};
		return response;
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