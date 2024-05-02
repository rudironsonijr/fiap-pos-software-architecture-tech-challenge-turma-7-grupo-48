using Domain.Entities.Exceptions;

namespace Domain.Entities.OrderAggregate
{
	public class OrderProduct
	{
		public int Id { get; init; }
		public required int ProductId { get; init; }

		private decimal _productPrice;
		public required decimal ProductPrice
		{
			get => _productPrice;
			init
			{
				EntityArgumentNumberInvalidException.ThrowIfLessOrEqualZero(
					value,
					nameof(ProductPrice),
					GetType());

				_productPrice = value;
			}
		}

		private int _quantity;
		public required int Quantity
		{
			get => _quantity;
			set
			{
				EntityArgumentNumberInvalidException.ThrowIfLessOrEqualZero(
					value,
					nameof(Quantity),
					GetType());

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
}
