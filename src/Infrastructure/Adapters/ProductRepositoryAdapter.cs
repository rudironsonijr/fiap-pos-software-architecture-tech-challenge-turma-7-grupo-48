using Domain.Entities.ProductAggregate;
using Domain.Repositories;

namespace Infrastructure.Adapters;

public class ProductRepositoryAdapter : IProductRepository
{
	public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Product> GetAsync(int Id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Product> UpdateAsync(Product Product, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
