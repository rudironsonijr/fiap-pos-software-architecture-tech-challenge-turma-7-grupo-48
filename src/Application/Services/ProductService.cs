using Application.Dtos.CustomerRequest;
using Application.Dtos.OrderRequest;
using Application.Dtos.ProductRequest;
using Application.Dtos.ProductRequest.Extensions;
using Application.Dtos.ProductResponse;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.OrderAggregate;
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

	public async Task<ProductCreateResponse?> CreateAsync(ProductCreateRequest productCreateRequest,
	CancellationToken cancellationToken)
	{

		_notificationContext.AssertArgumentNotNullOrWhiteSpace(productCreateRequest.Name, $"The field name is required");
		_notificationContext.AssertArgumentNotNullOrWhiteSpace(productCreateRequest.Description, $"The field Description is required");
		_notificationContext.AssertArgumentMinimumLength(productCreateRequest.Price, 0, "The product price can't be 0");

		if(_notificationContext.HasErrors)
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

	public async Task UpdatePriceAsync(ProductUpdatePriceRequest productUpdatePrice, CancellationToken cancellationToken)
	{
		//ToDo: Implementar validações
		var product = await _productRepository.GetAsync(productUpdatePrice.Id, cancellationToken);
		product.Price = productUpdatePrice.Price;
		await _productRepository.UpdateAsync(product, cancellationToken);
	}

	public async Task AddPhoto(int id, Photo photo, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetAsync(id, cancellationToken);

		_notificationContext.AssertArgumentNotNull(product, $"Product with id:{id} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		product.AddPhoto(photo);

		await _productRepository.UpdateAsync(product, cancellationToken);
	}
}