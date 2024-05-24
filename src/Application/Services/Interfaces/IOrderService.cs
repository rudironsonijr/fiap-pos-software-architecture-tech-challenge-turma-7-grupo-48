using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;
using Domain.Entities.Enums;

namespace Application.Services.Interfaces;

public interface IOrderService
{
	Task<GetOrListOrderResponse?> GetAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
	Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken);

	Task<OrderUpdateOrderProductResponse?> AddProduct(int OrderId, OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken);

	Task<OrderUpdateOrderProductResponse?> RemoveProduct(int orderId, int orderProductId,
		CancellationToken cancellationToken);

	Task<OrderUpdateOrderProductResponse?> UpdateProductQuantity(int orderId, int orderProductId,
		OrderUpdateProductQuantityRequest orderUpdateProductQuantityRequest, CancellationToken cancellationToken);
	Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken);
	Task CancelOrder(int orderId, CancellationToken cancellationToken);
}