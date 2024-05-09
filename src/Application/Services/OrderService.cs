using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;
using Application.Extensions.OrderAggregate;
using Application.Services.Interfaces;
using Core.Notifications;
using Domain.Entities.CustomerAggregate;
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
	public async Task<GetOrderResponse?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetAsync(id, cancellationToken);

		return
			order.ToGetOrderResponse();
	}

	public async Task<CreateOrderResponse?> CreateAsync(
		CreateOrderRequest orderCreateRequest,
		CancellationToken cancellationToken
	)
	{
		Customer? customer = null;
		var product = await _productRepository.GetAsync(orderCreateRequest.Product.ProductId, cancellationToken);

		if (orderCreateRequest.CustomerId != null)
		{
			customer = await _customerRepository.GetAsync(orderCreateRequest.CustomerId, cancellationToken);
			_notificationContext.AssertArgumentNotNull(customer, $"Customer with id: {orderCreateRequest.CustomerId} not found");
		}

		_notificationContext.AssertArgumentNotNull(product, $"Product with id:{orderCreateRequest.Product.ProductId} not found");
		_notificationContext.AssertArgumentMinimumLength(orderCreateRequest.Product.Quantity, 1, "The minimun quantity is 1");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		Order order = new()
		{
			CustomerId = customer?.Id,
		};

		order.AddProduct(product, orderCreateRequest.Product.Quantity);

		order = await _orderRepository.CreateAsync(order, cancellationToken);

		CreateOrderResponse response = new()
		{
			OrderId = order.Id
		};
		return response;
	}

	public async Task<OrderUpdateOrderProductResponse?> AddProduct(
		int orderId,
		OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken
	)
	{
		var orderTask = _orderRepository.GetAsync(orderId, cancellationToken);
		var productTask = _productRepository.GetAsync(orderAddProductRequest.ProductId, cancellationToken);

		await Task.WhenAll(productTask, orderTask);

		var order = orderTask.Result;
		var product = productTask.Result;

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");
		_notificationContext.AssertArgumentNotNull(product, $"Product with id:{orderAddProductRequest.ProductId} not found");
		_notificationContext.AssertArgumentMinimumLength(orderId, 1, "The minimun quantity is 1");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

		order.AddProduct(product, orderAddProductRequest.Quantity);

		order = await _orderRepository.UpdateAsync(order, cancellationToken);

		return
			order.ToOrderUpdateProductResponse();
	}

	public async Task<OrderUpdateOrderProductResponse?> RemoveProduct(
		int orderId,
		int orderProductId,
		CancellationToken cancellationToken
	)
	{
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);
		var orderProduct = order?.OrderProducts
			.Where(p => p.Id == orderProductId)
			.FirstOrDefault();

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");
		_notificationContext.AssertArgumentNotNull(orderProduct, $"orderProduct with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

#pragma warning disable CS8602 // Dereference of a possibly null reference.
		order.RemoveProduct(orderProductId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

		order = await _orderRepository.UpdateAsync(order, cancellationToken);

		return
			order.ToOrderUpdateProductResponse();
	}

	public async Task<OrderUpdateOrderProductResponse?> UpdateProductQuantity(
		int orderId,
		int orderProductId,
		OrderUpdateProductQuantityRequest orderUpdateProductQuantityRequest,
		CancellationToken cancellationToken
	)
	{

		var order = await _orderRepository.GetAsync(orderId, cancellationToken);
		var orderProduct = order?.OrderProducts
			.Where(p => p.Id == orderProductId)
			.FirstOrDefault();

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");
		_notificationContext.AssertArgumentNotNull(orderProduct, $"orderProduct with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return null;
		}

#pragma warning disable CS8602 // Dereference of a possibly null reference.
		order.UpdateProductQuantity(orderProductId, orderUpdateProductQuantityRequest.Quantity);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

		order = await _orderRepository.UpdateAsync(order, cancellationToken);

		return order
			.ToOrderUpdateProductResponse();
	}

	public async Task CancelOrder(int orderId, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		_notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

		if (_notificationContext.HasErrors)
		{
			return;
		}

		order.ChangeStatusToCancelled();

		await _orderRepository.UpdateAsync(order, cancellationToken);
	}
}