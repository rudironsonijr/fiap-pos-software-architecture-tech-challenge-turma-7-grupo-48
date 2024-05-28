using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;
using Application.Extensions.OrderAggregate;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.CustomerAggregate;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;

namespace Application.Services;

public class OrderService : IOrderService
{
	private readonly ICustomerRepository _customerRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IProductRepository _productRepository;
	private readonly NotificationContext _notificationContext;

	public OrderService(
		IOrderRepository orderRepository,
		ICustomerRepository customerRepository,
		IProductRepository productRepository,
		NotificationContext notificationContext
	)
	{
		_orderRepository = orderRepository;
		_customerRepository = customerRepository;
		_productRepository = productRepository;
		_notificationContext = notificationContext;
	}
	public async Task<GetOrListOrderResponse?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetAsync(id, cancellationToken);

		return
			order?.ToGetOrderResponse();
	}

	public async Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.ListAsync(orderStatus, page, limit, cancellationToken);

		return
			order.Select(x => x.ToGetOrderResponse());
	}

	public async Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest,
		CancellationToken cancellationToken)
	{
		Customer? customer = null;
		var product = await _productRepository.GetAsync(orderCreateRequest.Product.ProductId, cancellationToken);

		if (orderCreateRequest.CustomerId != null)
		{
			customer = await _customerRepository.GetAsync(orderCreateRequest.CustomerId.Value, cancellationToken);

			_notificationContext.AssertArgumentNotNull(customer, $"Customer with id: {orderCreateRequest.CustomerId} not found");
		}

		_notificationContext
			.AssertArgumentNotNull(product, $"Product with id:{orderCreateRequest.Product.ProductId} not found")
			.AssertArgumentIsMinimumLengthOrLess(orderCreateRequest.Product.Quantity, 0, "The minimun quantity is 1");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		Order order = new()
		{
			CustomerId = customer?.Id,
		};

		order.AddProduct(product!, orderCreateRequest.Product.Quantity);

		var orderId = await _orderRepository.CreateAsync(order, cancellationToken);

		return new()
		{
			OrderId = orderId
		};
		
	}

	public async Task<OrderUpdateOrderProductResponse?> AddProduct(int orderId,
		OrderAddProductRequest orderAddProductRequest, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);
		var product = await _productRepository.GetAsync(orderAddProductRequest.ProductId, cancellationToken);

		_notificationContext
			.AssertArgumentNotNull(order, $"Order with id:{orderId} not found")
			.AssertArgumentNotNull(product, $"Product with id:{orderAddProductRequest.ProductId} not found")
			.AssertArgumentIsMinimumLengthOrLess(orderId, 0, "The minimun quantity is 1");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		order!.AddProduct(product!, orderAddProductRequest.Quantity);

		await _orderRepository.UpdateAsync(order, cancellationToken);

		return
			order.ToOrderUpdateProductResponse();
	}

	public async Task<OrderUpdateOrderProductResponse?> RemoveProduct(int orderId,
		int productId, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);
		var orderProduct = order?.OrderProducts
			.Where(p => p.ProductId == productId)
			.FirstOrDefault();

		_notificationContext
			.AssertArgumentNotNull(order, $"Order with id:{orderId} not found")
			.AssertArgumentNotNull(orderProduct, $"orderProduct with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		order!.RemoveProduct(productId);

		await _orderRepository.UpdateAsync(order, cancellationToken);

		return
			order.ToOrderUpdateProductResponse();
	}

	public async Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken)
	{

		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		order!.ChangeStatusToPreparing();

		await _orderRepository.UpdateAsync(order, cancellationToken);
	}

	public async Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken)
	{

		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		order!.ChangeStatusToDone();

		await _orderRepository.UpdateAsync(order, cancellationToken);
	}

	public async Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken)
	{

		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		order!.ChangeStatusToFinished();

		await _orderRepository.UpdateAsync(order, cancellationToken);
	}

	public async Task CancelOrder(int orderId, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		order!.ChangeStatusToCancelled();

		await _orderRepository.UpdateAsync(order, cancellationToken);
	}
}