using Domain.Exceptions;

namespace Domain.ValueObjects.Exceptions;

public class InvalidEmailException(string address)
	: DomainException(string.Format(DefaultInvalidEmailMessageTemplate, address))
{
	private const string DefaultInvalidEmailMessageTemplate = "The Email Address {0} is invalid";
}