using Domain.Entities.OrderAggregate;

namespace Domain.Repositories;

public interface IOrderRepository
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