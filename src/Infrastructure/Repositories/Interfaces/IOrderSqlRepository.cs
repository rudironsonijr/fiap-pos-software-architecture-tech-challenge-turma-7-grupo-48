using Infrastructure.Repositories.Base;
using Infrastructure.SqlModels.OrderAggregate;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces;

public interface IOrderSqlRepository : ISqlRepository
{
	Task<IEnumerable<OrderSqlModel>> ListAsync(Expression<Func<OrderSqlModel, bool>> expression,
		int? page, int? limit, CancellationToken cancellationToken);
	Task<OrderSqlModel?> GetAsync(Expression<Func<OrderSqlModel, bool>> expression, 
		bool asNoTracking, CancellationToken cancellationToken);
	OrderSqlModel Add(OrderSqlModel order);
	void Update(OrderSqlModel order);
}
