using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;

namespace Application.Services.Interfaces;

public interface IOrderService
{
	Task<OrderCreateResponse> CreateAsync(
		OrderCreateRequest orderCreateRequest,
		CancellationToken cancellationToken
	);

	Task<OrderUpdateOrderProductResponse?> AddProduct(
		int OrderId,
		OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken
	);

	Task<OrderUpdateOrderProductResponse?> RemoveProduct(
		int orderId,
		int orderProductId,
		CancellationToken cancellationToken
	);

	Task<OrderUpdateOrderProductResponse?> UpdateProductQuantity(
		int orderId,
		int orderProductId,
		OrderUpdateProductQuantityRequest orderUpdateProductQuantityRequest,
		CancellationToken cancellationToken
	);

	Task CancelOrder(
		int orderId,
		CancellationToken cancellationToken
	);
}