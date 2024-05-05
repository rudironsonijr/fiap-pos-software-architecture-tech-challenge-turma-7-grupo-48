using Domain.Entities.ProductAggregate;

namespace Domain.Repositories;

public interface IProductRepository
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