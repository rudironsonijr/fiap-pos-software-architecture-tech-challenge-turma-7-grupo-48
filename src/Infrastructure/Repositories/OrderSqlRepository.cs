using Domain.Repositories.Base;
using Helpers;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class OrderSqlRepository : IOrderSqlRepository
{
	private readonly DinersSqlContext _context;
	public IUnitOfWork UnitOfWork => _context;
	public OrderSqlRepository(DinersSqlContext dinersSqlContext)
	{
		_context = dinersSqlContext;
	}

	public async Task<IEnumerable<OrderSqlModel>> ListAsync(Expression<Func<OrderSqlModel, bool>> expression,
		int? page, int? limit, CancellationToken cancellationToken)
	{
		var take = limit ?? int.MaxValue;

		var skip = MathHelper.CalculatePaginateSkip(page, take);

		var orders = await _context.Order
			.AsNoTracking()
			.Include(x => x.OrderProducts)
			.ThenInclude(x => x.Product)
			.Where(expression)
			.Skip(skip)
			.Take(take)
			.ToArrayAsync(cancellationToken);

		return orders ?? Enumerable.Empty<OrderSqlModel>();
	}

	public async Task<OrderSqlModel?> GetAsync(Expression<Func<OrderSqlModel, bool>> expression,
		bool asNoTracking, CancellationToken cancellationToken)
	{

		if (asNoTracking)
		{
			return await _context.Order
				.Include(x => x.OrderProducts)
				.ThenInclude(x => x.Product)
				.AsNoTracking()
				.FirstOrDefaultAsync(expression, cancellationToken);
		}

		return await _context.Order
			.Include(x => x.OrderProducts)
			.ThenInclude(x => x.Product)
			.FirstOrDefaultAsync(expression, cancellationToken);
	}

	public void Update(OrderSqlModel order)
	{
		_context.Order
			.Update(order);
	}

	public OrderSqlModel Add(OrderSqlModel order)
	{
		return
			_context.Order
			.Add(order).Entity;
	}
}
