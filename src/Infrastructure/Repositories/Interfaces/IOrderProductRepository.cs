using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.Repositories.Interfaces;

public interface IOrderProductRepository
{
	void Update(OrderProductSqlModel order);
	OrderProductSqlModel Add(OrderProductSqlModel order);
	void Remove(IEnumerable<OrderProductSqlModel> order);
}
