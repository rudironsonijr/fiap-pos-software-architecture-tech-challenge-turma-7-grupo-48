using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;
using Application.Services.Interfaces;
using Domain.Entities.CustomerAggregate;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;

namespace Application.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly ICustomerRepository _customerRepository;
	private readonly IProductRepository _productRepository;
	public OrderService(
		IOrderRepository orderRepository,
		ICustomerRepository customerRepository,
		IProductRepository productRepository)
	{
		_orderRepository = orderRepository;
		_customerRepository = customerRepository;
		_productRepository = productRepository;
	}

	public async Task<OrderCreateResponse> CreateAsync(
		OrderCreateRequest orderCreateRequest,
		CancellationToken cancellationToken)
	{
		//Todo: Validar se a OrderAddProduct não veio vazia
		var customerTask = GetCustomer(
			orderCreateRequest.CustomerCpf,
			cancellationToken);
		var productTask = _productRepository.GetAsync(
			orderCreateRequest.Product.ProductId,
			cancellationToken
			);

		await Task.WhenAll(productTask, customerTask);

		//ToDo: validar caso produto não encontrado
		var product = productTask.Result;
		var customer = customerTask.Result;

		Order order = new()
		{
			CustomerId = customer?.Id,
		};

		order.AddProduct(
			product,
			orderCreateRequest.Product.Quantity
			);

		order = await _orderRepository.CreateAsync(order, cancellationToken);

		OrderCreateResponse response = new()
		{
			OrderId = order.Id
		};
		throw new NotImplementedException();
		return response;

	}

	public async Task<OrderUpdateProductResponse> AddProduct(
		int orderId,
		OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken)
	{
		var orderTask = _orderRepository.GetAsync(orderId, cancellationToken);
		var productTask = _productRepository.GetAsync(
			orderAddProductRequest.ProductId,
			cancellationToken
			);

		await Task.WhenAll(productTask, orderTask);

		//ToDo: validar caso order não encontrado
		var order = orderTask.Result;
		//ToDo: validar caso produto não encontrado
		var product = productTask.Result;

		order.AddProduct(product, orderAddProductRequest.Quantity);

		order = await _orderRepository.UpdateAsync(order, cancellationToken);

		throw new NotImplementedException();
		//ToDo: Fazer o mapeamento do response
		return new OrderUpdateProductResponse();
	}

	public async Task<OrderUpdateProductResponse> RemoveProduct(
	int orderId,
	int orderProductId,
	CancellationToken cancellationToken)
	{
		//ToDo: validar caso order não encontrado
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		order.RemoveProduct(orderProductId);

		order = await _orderRepository.UpdateAsync(order, cancellationToken);

		throw new NotImplementedException();
		//ToDo: Fazer o mapeamento do response
		return new OrderUpdateProductResponse();
	}

	public async Task<OrderUpdateProductResponse> UpdateProductQuantity(
		int orderId,
		int orderProductId,
		OrderUpdateProductQuantityRequest orderUpdateProductQuantityRequest,
		CancellationToken cancellationToken)
	{
		//ToDo: validar caso order não encontrado
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		order.UpdateProductQuantity(orderProductId, orderUpdateProductQuantityRequest.Quantity);

		order = await _orderRepository.UpdateAsync(order, cancellationToken);

		throw new NotImplementedException();
		//ToDo: Fazer o mapeamento do response
		return new OrderUpdateProductResponse();
	}

	public async Task CancelOrder(
		int orderId,
		CancellationToken cancellationToken)
	{
		//ToDo: validar caso order não encontrado
		var order = await _orderRepository.GetAsync(orderId, cancellationToken);

		order.ChangeStatusToCancelled();

		await _orderRepository.UpdateAsync(order, cancellationToken);
	}

	private async Task<Customer?> GetCustomer(string? cpf, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(cpf))
			return null;

		var customer = await _customerRepository.GetByCpf(cpf, cancellationToken);
		if (customer == null)
		{
			//ToDo: Implementar validação caso não encontre o Customer
			throw new NotImplementedException();
		}

		return customer;

	}
}
