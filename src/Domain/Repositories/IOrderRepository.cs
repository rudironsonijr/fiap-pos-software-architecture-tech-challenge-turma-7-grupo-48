using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
	Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);
	Task UpdateAsync(Order order, CancellationToken cancellationToken);
	Task<Order?> GetAsync(int id, CancellationToken cancellationToken);
	Task<IEnumerable<Order>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
	Task<IEnumerable<Order>> ListAsync(IEnumerable<OrderStatus> orderStatus, int? page,
	   int? limit, CancellationToken cancellationToken);
}