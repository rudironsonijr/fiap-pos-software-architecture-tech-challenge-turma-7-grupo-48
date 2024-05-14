using Domain.Entities.ProductAggregate;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface IProductRepository: IRepository<Product>
{
	Task<Product> CreateAsync(
		Product product,
		CancellationToken cancellationToken
	);

	Task<Product> GetAsync(
		int Id,
		CancellationToken cancellationToken
	);

	Task<Product> UpdateAsync(
		Product Product,
		CancellationToken cancellationToken
	);
}