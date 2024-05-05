using Domain.Entities.Base.Exceptions;

namespace Domain.Entities.OrderAggregate;

public class OrderProduct
{
	private readonly decimal _productPrice;

	private int _quantity;
	public int Id { get; init; }
	public required int ProductId { get; init; }

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