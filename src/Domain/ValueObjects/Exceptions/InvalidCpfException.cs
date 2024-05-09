using Domain.Exceptions;

namespace Domain.ValueObjects.Exceptions;

public class InvalidCpfException(
	string number
) : DomainException(
	string.Format(
		DefaultInvalidEmailMessageTemplate,
		number
	)
)
{
	private const string DefaultInvalidEmailMessageTemplate = "The CPF number {0} is invalid";
}