using Domain.Repositories.Base;
using Helpers;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.OrderAggregate;
using Infrastructure.SqlModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductSqlRepository : IProductSqlRepository
{
	private readonly DinersSqlContext _context;
	public IUnitOfWork UnitOfWork => _context;
	public ProductSqlRepository(DinersSqlContext dinersSqlContext)
	{
		_context = dinersSqlContext;
	}

	public async Task<IEnumerable<ProductSqlModel>> ListAsync(Expression<Func<ProductSqlModel, bool>> expression,
		int? page, int? limit, CancellationToken cancellationToken)
	{
		var take = limit ?? int.MaxValue;

		var skip = MathHelper.CalculatePaginateSkip(page, take);

		var orders = await _context.Product
			.AsNoTracking()
			.Where(expression)
			.Skip(skip)
			.Take(take)
			.ToArrayAsync(cancellationToken);

		return orders ?? Enumerable.Empty<ProductSqlModel>();
	}

	public async Task<ProductSqlModel?> GetAsync(Expression<Func<ProductSqlModel, bool>> expression,
		bool asNoTracking, CancellationToken cancellationToken)
	{

		if (asNoTracking)
		{
			return await _context.Product
				.Include(x => x.OrderProducts)
				.AsNoTracking()
				.FirstOrDefaultAsync(expression, cancellationToken);
		}

		return await _context.Product
			.FirstOrDefaultAsync(expression, cancellationToken);
	}

	public void Update(ProductSqlModel order)
	{
		_context.Product
			.Update(order);
	}

	public ProductSqlModel Add(ProductSqlModel product)
	{
		return
			_context.Product
				.Add(product).Entity;
	}
}
