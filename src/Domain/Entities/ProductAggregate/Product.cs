using Domain.Entities.Base.Exceptions;
using Domain.Entities.Base.Interfaces;
using Domain.Entities.Enums;

namespace Domain.Entities.ProductAggregate;
//ToDO: implementar Photo
public class Product : IAggregateRoot
{
	public int Id { get; init; }

	private string _name = string.Empty;
	public required string Name
	{
		get => _name;
		init
		{
			EntityArgumentNullException.ThrowIfNullOrWhiteSpace(
				value, 
				nameof(Name), 
				GetType());

			_name = value;
		}
	}

	private string _description = string.Empty;
	public required string Description
	{
		get => _description;
		set
		{
			EntityArgumentNullException.ThrowIfNullOrWhiteSpace(
				value, 
				nameof(Description), 
				GetType());

			_description = value;
		}
	}
	private ProductType _productType;
	public ProductType ProductType
	{
		get => _productType;
		init
		{
			if (ProductType == ProductType.None)
			{
				throw new EntityArgumentEnumInvalidException(
					nameof(ProductType),
					ProductType.None.ToString(),
					GetType().ToString());
			}
			_productType = value;
		}
	}
	public decimal _price;
	public decimal Price
	{
		get => _price;
		set
		{
			EntityArgumentNumberInvalidException.ThrowIfLessOrEqualZero(
				value, 
				nameof(Price), 
				GetType());	

			_price = value;
		}
	}
}
