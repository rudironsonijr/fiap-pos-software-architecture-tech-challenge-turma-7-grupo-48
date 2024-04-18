namespace Domain.ValueObjects.Exceptions;

public class InvalidCpfException : Exception
{
    const string DEFAULT_INVALID_EMAIL_MESSAGE_TEMPLATE = "The CPF number {0} is invalid";
    public InvalidCpfException(string number) : base(string.Format(DEFAULT_INVALID_EMAIL_MESSAGE_TEMPLATE, number))
    {

    }
}
