using Domain.Entities.OrderAggregate;
using Domain.Repositories;

namespace Infrastructure.Adapters;

public class OrderRepositoryAdpater : IOrderRepository
{
	public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Order> GetAsync(int id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
