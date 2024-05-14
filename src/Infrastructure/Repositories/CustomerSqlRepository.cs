using Domain.Repositories.Base;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerSqlRepository : ICustomerSqlRepository
{
	private readonly DinersSqlContext _context;
	public IUnitOfWork UnitOfWork => _context;
	public CustomerSqlRepository(DinersSqlContext dinersSqlContext)
	{
		_context = dinersSqlContext;
	}

	public async Task<CustomerSqlModel?> GetAsync(Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken)
	{
		return await _context.Customer
			.AsNoTracking()
			.SingleOrDefaultAsync(expression, cancellationToken);
	}

	public Task<int> CountAsync(Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken)
	{
		return _context.Customer.CountAsync(expression, cancellationToken);
	}

	public CustomerSqlModel Add(CustomerSqlModel customer)
	{
		return
			_context.Customer
			.Add(customer)
			.Entity;
	}

}