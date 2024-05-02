namespace Domain.Entities.OrderAggregate.Exceptions;
public class ChangeOrderStatusToReceivedException : Exception
{
	private ChangeOrderStatusToReceivedException(string message) : base(message)
	{

	}
	public static void ThrowIfOrderProductsIsEmpty(List<OrderProduct> orderProducts)
	{
		if (orderProducts == null || orderProducts.Any() is false)
		{
			throw new ChangeOrderStatusToReceivedException("At Least one product is required to sent the order");
		}
	}

}
