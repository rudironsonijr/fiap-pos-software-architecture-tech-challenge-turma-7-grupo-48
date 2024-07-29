using UseCase.Dtos.OrderRequest;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;

namespace UseCase.Services.Interfaces;

public interface IOrderUseCase
{
	Task<Order?> GetAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<Order>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
	Task<IEnumerable<Order>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken);
	Task<Order?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken);

	Task<Order?> AddProduct(int OrderId, OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken);

	Task<Order?> RemoveProduct(int orderId, int productId,
		CancellationToken cancellationToken);

	Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken);
	Task CancelOrder(int orderId, CancellationToken cancellationToken);
}