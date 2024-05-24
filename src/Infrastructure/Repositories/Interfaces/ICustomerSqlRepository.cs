using System.Linq.Expressions;
using Infrastructure.Repositories.Base;
using Infrastructure.SqlModels.CustomerAggregate;

namespace Infrastructure.Repositories.Interfaces;

public interface ICustomerSqlRepository : ISqlRepository
{
	Task<CustomerSqlModel?> GetAsync(Expression<Func<CustomerSqlModel, bool>> expression,
			bool asNoTracking, CancellationToken cancellationToken);

	Task<int> CountAsync(Expression<Func<CustomerSqlModel, bool>> expression,
		CancellationToken cancellationToken);

	CustomerSqlModel Add(CustomerSqlModel customer);
}