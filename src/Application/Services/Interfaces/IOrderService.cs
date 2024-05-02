using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;

namespace Application.Services.Interfaces;

public interface IOrderService
{
	Task<OrderCreateResponse> CreateAsync(
		OrderCreateRequest orderCreateRequest,
		CancellationToken cancellationToken);

	Task<OrderUpdateProductResponse> AddProduct(
		int OrderId,
		OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken);
	Task<OrderUpdateProductResponse> RemoveProduct(
	   int orderId,
	   int orderProductId,
	   CancellationToken cancellationToken);
	Task<OrderUpdateProductResponse> UpdateProductQuantity(
		int orderId,
		int orderProductId,
		OrderUpdateProductQuantityRequest orderUpdateProductQuantityRequest,
		CancellationToken cancellationToken);
}
