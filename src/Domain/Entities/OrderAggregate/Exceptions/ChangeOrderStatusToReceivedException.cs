using Domain.Entities.Enums;
using Domain.Exceptions;
using Helpers;

namespace Domain.Entities.OrderAggregate.Exceptions;
internal class ChangeOrderStatusInvalidException : DomainException
{
	private const string MessageThrowIfOrderStatusInvalidStepChangeTemplate = "Is not possible change a order in status {1} to {2}";
	private ChangeOrderStatusInvalidException(string message) : base(message)
	{

	}
	public static void ThrowIfOrderProductsIsEmpty(List<OrderProduct> orderProducts)
	{
		if (orderProducts == null || orderProducts.Any() is false)
		{
			throw new ChangeOrderStatusInvalidException("At Least one product is required to sent the order");
		}
	}

	public static void ThrowIfOrderStatusInvalidStepChange(
		OrderStatus ActualStatus, 
		OrderStatus ExpectedStatus, 
		OrderStatus NewStatus)
	{
		if (!ActualStatus.Equals(ExpectedStatus))
		{
			throw new ChangeOrderStatusInvalidException(
				string.Format(
					MessageThrowIfOrderStatusInvalidStepChangeTemplate,
					ActualStatus.GetEnumDescription(),
					NewStatus.GetEnumDescription()));
		}
	}

}
