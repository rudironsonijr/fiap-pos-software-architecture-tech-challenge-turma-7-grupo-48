using System.Linq.Expressions;
using Infrastructure.Repositories.Base;
using Infrastructure.SqlModels;

namespace Infrastructure.Repositories.Interfaces;

public interface ICustomerSqlRepository : ISqlRepository
{
	Task<CustomerSqlModel?> GetAsync(
		Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken
	);

	Task<int> CountAsync(
		Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken
	);

	CustomerSqlModel Add(CustomerSqlModel customer);
}