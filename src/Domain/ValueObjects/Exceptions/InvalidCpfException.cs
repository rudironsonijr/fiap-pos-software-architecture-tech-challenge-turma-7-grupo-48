using Domain.Exceptions;

namespace Domain.ValueObjects.Exceptions;

public class InvalidCpfException(string number) 
	: DomainException(string.Format(DefaultMessageTemplate,number))
{
	private const string DefaultMessageTemplate = "The CPF number {0} is invalid";
}