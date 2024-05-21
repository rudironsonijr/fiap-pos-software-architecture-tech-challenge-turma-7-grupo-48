using Domain.Entities.Enums;
using Domain.Exceptions;
using Helpers;

namespace Domain.ValueObjects.Exceptions;

public class PaymentMethodKindNotSupportedException : DomainException
{
	private const string DefaultMessage = "The Payment Method {0} is not supported for provider {1}";

	public PaymentMethodKindNotSupportedException(PaymentMethodKind kind, PaymentProvider provider) 
		: base(string.Format(DefaultMessage, kind.GetEnumDescription(), provider.GetEnumDescription()))
	{
		
	}
}
