using Controller.Application.Interfaces;
using UseCase.Dtos.OrderRequest;
using Controller.Dtos.OrderResponse;
using Controller.Extensions.OrderAggregate;
using Domain.Entities.Enums;
using Domain.Repositories;
using UseCase.Services.Interfaces;

namespace Controller.Application;

public class OrderApplication : IOrderApplication
{
	IOrderUseCase _orderUseCase;
	public OrderApplication(IOrderUseCase orderUseCase)
	{
		_orderUseCase = orderUseCase;
	}

	public async Task<GetOrListOrderResponse?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.GetAsync(id, cancellationToken);

		return
			order?.ToGetOrderResponse();
	}

	public async Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.ListAsync(orderStatus, page, limit, cancellationToken);

		return
			order.Select(x => x.ToGetOrderResponse());
	}

	public async Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken)
	{
		var orderId = await _orderUseCase.CreateAsync(orderCreateRequest, cancellationToken);
		if (orderId == null)
		{
			return null;
		}

		return new()
		{
			OrderId = orderId.Value
		};
	}

	public async Task<OrderUpdateOrderProductResponse?> AddProduct(int orderId,
		OrderAddProductRequest orderAddProductRequest, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.AddProduct(orderId, orderAddProductRequest, cancellationToken);

		return
			order?.ToOrderUpdateProductResponse();

	}
	public async Task<OrderUpdateOrderProductResponse?> RemoveProduct(int orderId,
		int productId, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.RemoveProduct(orderId, productId, cancellationToken);
		return
			order?.ToOrderUpdateProductResponse();
	}

	public Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.UpdateStatusToPreparing(orderId, cancellationToken);
	}

	public Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.UpdateStatusToDone(orderId, cancellationToken);
	}

	public Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.UpdateStatusToFinished(orderId, cancellationToken);
	}

	public Task CancelOrder(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.CancelOrder(orderId, cancellationToken);
	}
}
