using Domain.Entities.Enums;
using Domain.Entities.ProductAggregate;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.Extensions.ProductAggregate;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.ProductAggregate.Extensions;

namespace Infrastructure.Adapters;

public class ProductRepositoryAdapter : IProductRepository
{
	private readonly IProductSqlRepository _repository;

	public ProductRepositoryAdapter(IProductSqlRepository productSqlRepository)
	{
		_repository = productSqlRepository;
	}

	public async Task<Product?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var productSql = await _repository.GetAsync(x => x.Id == id, true, cancellationToken);
		return
			productSql?.ToProduct();
	}

	public async Task<IEnumerable<Product>> ListAsync(ProductType productType, int? page,
		int? limit, CancellationToken cancellationToken)
	{
		var productSql = await _repository.ListAsync(x => x.ProductType == productType,
			page, limit, cancellationToken);

		var response = productSql.Select(x => x.ToProduct());
		return response;

	}

	public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
	{
		var productSqlModel = product.ToProductSqlModel();
		_repository.Add(productSqlModel);
		await _repository.UnitOfWork.CommitAsync(cancellationToken);

		return productSqlModel.ToProduct();
	}

	public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
	{
		var productSql = await _repository.GetAsync(x => x.Id == product.Id, false, cancellationToken);

		EntityNotFoundException.ThrowIfPropertyNull(productSql, typeof(Product), "Id", product.Id); ;

		productSql!.Price = product.Price;
		productSql!.Description = product.Description;
		productSql.PhotoContentType = product.Photo?.ContentType;
		productSql.PhotoData = product.Photo?.Data;
		productSql.PhotoFilename = product.Photo?.FileName;

		_repository.Update(productSql);
		await _repository.UnitOfWork.CommitAsync(cancellationToken);

	}
}
