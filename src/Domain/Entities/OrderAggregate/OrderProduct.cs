using Domain.Entities.Base.Exceptions;
using Domain.Entities.ProductAggregate;

namespace Domain.Entities.OrderAggregate;

public class OrderProduct
{
	public OrderProduct(Product product)
	{
		Product = product;
		Id = product.Id;
	}
	private readonly decimal _productPrice;

	private int _quantity;
	public int Id { get; private init; }
	public int ProductId { get; init; }
	public Product Product { get; init; }

	public required decimal ProductPrice
	{
		get => _productPrice;
		init
		{
			EntityArgumentNumberInvalidException.ThrowIfLessOrEqualZero(
				value,
				nameof(ProductPrice),
				GetType()
			);

			_productPrice = value;
		}
	}

	public required int Quantity
	{
		get => _quantity;
		set
		{
			EntityArgumentNumberInvalidException.ThrowIfLessOrEqualZero(
				value,
				nameof(Quantity),
				GetType()
			);

			_quantity = value;
		}
	}

	public decimal Price
	{
		get
		{
			var total = _productPrice * _quantity;
			return Math.Round(total, 2);
		}
	}
}