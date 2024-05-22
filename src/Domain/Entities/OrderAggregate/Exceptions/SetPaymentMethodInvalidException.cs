using Domain.Exceptions;

namespace Domain.Entities.OrderAggregate.Exceptions;

public class SetPaymentMethodInvalidException() : DomainException(DefaultChangeOrderProductNotAbleMessageTemplate)
{
	private const string DefaultChangeOrderProductNotAbleMessageTemplate =
		"Is not possiblea change payment method when order is not Creating";
}

