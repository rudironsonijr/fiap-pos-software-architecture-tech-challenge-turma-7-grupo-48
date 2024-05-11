using System.Linq.Expressions;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerSqlRepository : ICustomerSqlRepository
{
	private readonly DinersSqlContext _context;

	//public CustomerSqlRepository(SqlContext sqlContext)
	//{
	//	_context = sqlContext;
	//}

	public async Task<CustomerSqlModel?> GetAsync(
		Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken
	)
	{
		return await _context.Customer.FindAsync(
			expression,
			cancellationToken
		);
	}

	public Task<int> CountAsync(
		Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken
	)
	{
		return _context.Customer.CountAsync(
			expression,
			cancellationToken
		);
	}
}