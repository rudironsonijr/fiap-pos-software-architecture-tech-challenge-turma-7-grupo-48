namespace Domain.ValueObjects.Exceptions;

public class InvalidCpfException(
	string number
) : Exception(
	message: string.Format(
		format: DefaultInvalidEmailMessageTemplate,
		arg0: number
	)
)
{
	private const string DefaultInvalidEmailMessageTemplate = "The CPF number {0} is invalid";
}