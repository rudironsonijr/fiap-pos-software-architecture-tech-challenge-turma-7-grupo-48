using Domain.Repositories.Base;
using Helpers;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.PaymentAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class PaymentSqlModelRepository : IPaymentSqlModelRepository
{
	private readonly DinersSqlContext _context;
	public IUnitOfWork UnitOfWork => _context;
	public PaymentSqlModelRepository(DinersSqlContext dinersSqlContext)
	{
		_context = dinersSqlContext;
	}

	public PaymentSqlModel Add(PaymentSqlModel payment)
	{
		return
			_context.Payment
			.Add(payment)
			.Entity;
	}

	public void Update(PaymentSqlModel payment)
	{
		_context.Payment
			.Update(payment);
	}

	public async Task<IEnumerable<PaymentSqlModel>> ListAsync(Expression<Func<PaymentSqlModel, bool>> expression,
		int? page, int? limit, CancellationToken cancellationToken)
	{
		var take = limit ?? int.MaxValue;

		var skip = MathHelper.CalculatePaginateSkip(page, take);

		var orders = await _context.Payment
			.AsNoTracking()
			.Where(expression)
			.Skip(skip)
			.Take(take)
			.ToArrayAsync(cancellationToken);

		return orders ?? Enumerable.Empty<PaymentSqlModel>();
	}

	public async Task<PaymentSqlModel?> GetAsync(Expression<Func<PaymentSqlModel, bool>> expression,
		bool asNoTracking, CancellationToken cancellationToken)
	{

		if (asNoTracking)
		{
			return await _context.Payment
				.AsNoTracking()
				.FirstOrDefaultAsync(expression, cancellationToken);
		}

		return await _context.Payment
			.FirstOrDefaultAsync(expression, cancellationToken);
	}
}
