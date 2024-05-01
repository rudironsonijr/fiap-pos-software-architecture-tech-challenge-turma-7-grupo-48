using Domain.Entities.Enums;
using Domain.Entities.Exceptions;
using Domain.Entities.Helpers;

namespace Domain.Entities.ProductAggregate
{
	public class Product
	{
		public int Id { get; init; }

		private string _name = string.Empty;
		public required string Name
		{
			get => _name;
			init
			{
				EntityValidation.FailIfNullOrWhiteSpace(value, nameof(Name));
				_name = value;
			}
		}

		private string _description = string.Empty;
		public required string Description
		{
			get => _description;
			set
			{
				EntityValidation.FailIfNullOrWhiteSpace(value, nameof(Description));
				_description = value;
			}
		}
		private ProductType _productType;
		public ProductType ProductType 
		{ 
			get => _productType; 
			init
			{
				if(ProductType == ProductType.None)
				{
					throw new EntityArgumentEnumInvalidException(
						nameof(ProductType), 
						ProductType.None.ToString(),
						GetType().ToString());
				}
				_productType =  value;
			}
		}
		public decimal _price;
		public decimal Price
		{
			get => _price;
			set
			{
				EntityArgumentNumberInvalidException.ThrowIfLessOrEqualThan(
					0, 
					value,
					ProductType.None.ToString(),
					GetType().ToString());

				_price = value;
			}
		}
	}
}
