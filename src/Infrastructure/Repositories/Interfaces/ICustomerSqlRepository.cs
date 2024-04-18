using Infrastructure.SqlModels;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces;

public interface ICustomerSqlRepository
{
    Task<CustomerSqlModel?> GetAsync(Expression<Func<CustomerSqlModel, bool>> expression,
        CancellationToken cancellationToken);
}
