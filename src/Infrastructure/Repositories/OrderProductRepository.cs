using Domain.Repositories.Base;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.Repositories;

public class OrderProductRepository : IOrderProductRepository
{
	private readonly DinersSqlContext _context;
	public OrderProductRepository(DinersSqlContext dinersSqlContext)
	{
		_context = dinersSqlContext;
	}

	public void Update(OrderProductSqlModel order)
	{
		_context.OrderProduct
			.Update(order);
	}

	public OrderProductSqlModel Add(OrderProductSqlModel order)
	{
		return
			_context.OrderProduct
			.Add(order).Entity;
	}

	public void Remove(IEnumerable<OrderProductSqlModel> order)
	{
		_context.OrderProduct
			.RemoveRange(order);
	}
}
