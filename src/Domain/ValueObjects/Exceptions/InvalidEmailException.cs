namespace Domain.ValueObjects.Exceptions;

public class InvalidEmailException(
	string address
) : Exception(
	message: string.Format(
		format: DefaultInvalidEmailMessageTemplate,
		arg0: address
	)
)
{
	private const string DefaultInvalidEmailMessageTemplate = "The Email Address {0} is invalid";
}