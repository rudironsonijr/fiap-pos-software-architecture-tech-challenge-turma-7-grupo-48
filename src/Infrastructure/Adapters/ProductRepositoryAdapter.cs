using Domain.Entities.ProductAggregate;
using Domain.Repositories;
using Domain.Repositories.Base;

namespace Infrastructure.Adapters;

public class ProductRepositoryAdapter : IProductRepository
{
	public IUnitOfWork UnitOfWork => throw new NotImplementedException();

	public Task<int> CreateAsync(Product product, CancellationToken cancellationToken)
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
