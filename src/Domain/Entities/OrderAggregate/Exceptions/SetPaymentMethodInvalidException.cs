using Domain.Entities.Enums;
using Domain.Exceptions;

namespace Domain.Entities.OrderAggregate.Exceptions;

public class SetPaymentMethodInvalidException : DomainException
{
	private const string DefaultChangeOrderProductNotAbleMessageTemplate =
		"Is not possible change payment method when order is not Creating";

	public SetPaymentMethodInvalidException() : base(DefaultChangeOrderProductNotAbleMessageTemplate)
	{

	}
	
	public static void ThrowInvalidStatus(OrderStatus status)
	{
		if (status != OrderStatus.Creating)
		{
			throw new SetPaymentMethodInvalidException();
		}
	}
}

