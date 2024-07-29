using UseCase.Dtos.OrderRequest;
using Controller.Dtos.OrderResponse;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;

namespace Controller.Application.Interfaces;

public interface IOrderApplication
{
	Task<GetOrListOrderResponse?> GetAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
	Task<IEnumerable<GetOrListOrderResponse>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken);
	Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken);

	Task<OrderUpdateOrderProductResponse?> AddProduct(int OrderId, OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken);

	Task<OrderUpdateOrderProductResponse?> RemoveProduct(int orderId, int productId,
		CancellationToken cancellationToken);

	Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken);
	Task CancelOrder(int orderId, CancellationToken cancellationToken);
}
