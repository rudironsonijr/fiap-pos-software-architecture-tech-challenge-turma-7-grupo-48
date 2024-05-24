using Infrastructure.Repositories.Base;
using Infrastructure.SqlModels.ProductAggregate;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces;

public interface IProductSqlRepository : ISqlRepository
{
	Task<IEnumerable<ProductSqlModel>> ListAsync(Expression<Func<ProductSqlModel, bool>> expression,
		int? page, int? limit, CancellationToken cancellationToken);
	Task<ProductSqlModel?> GetAsync(Expression<Func<ProductSqlModel, bool>> expression,
		bool asNoTracking, CancellationToken cancellationToken);

	void Update(ProductSqlModel order);
	ProductSqlModel Add(ProductSqlModel product);
}
