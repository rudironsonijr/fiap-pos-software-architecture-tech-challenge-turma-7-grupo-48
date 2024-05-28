using Domain.Entities.Base.Interfaces;
using Domain.Entities.Enums;
using Domain.Entities.Exceptions;
using Domain.Entities.OrderAggregate;
using Domain.ValueObjects;

namespace Domain.Entities.ProductAggregate;


public class Product : IAggregateRoot
{
	public int Id { get; init; }

	private readonly string _name = string.Empty;
	public required string Name
	{
		get => _name;
		init
		{
			EntityArgumentNullException.ThrowIfNullOrWhiteSpace(value, nameof(Name), GetType());

			_name = value;
		}
	}

	private string _description = string.Empty;
	public required string Description
	{
		get => _description;
		set
		{
			EntityArgumentNullException.ThrowIfNullOrWhiteSpace(value, nameof(Description), GetType());

			_description = value;
		}

	}

	private readonly ProductType _productType;
	public required ProductType ProductType
	{
		get => _productType;
		init
		{
			if (value == ProductType.None)
			{
				throw new EntityArgumentEnumInvalidException(nameof(ProductType),
					ProductType.None.ToString(), GetType().ToString());
			}

			_productType = value;
		}
	}

	public decimal _price;
	public required decimal Price
	{
		get => _price;
		set
		{
			EntityArgumentNumberInvalidException.ThrowIfLessOrEqualZero(value, nameof(Price), GetType());

			_price = value;
		}
	}


	public Photo? Photo { get; set; }

}