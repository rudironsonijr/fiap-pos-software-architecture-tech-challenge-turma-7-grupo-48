using Domain.Entities.OrderAggregate;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
	Task<Order> CreateAsync(
		Order order,
		CancellationToken cancellationToken
	);

	Task<Order> UpdateAsync(
		Order order,
		CancellationToken cancellationToken
	);

	Task<Order> GetAsync(
		int id,
		CancellationToken cancellationToken
	);
}