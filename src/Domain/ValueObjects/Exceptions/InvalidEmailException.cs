namespace Domain.ValueObjects.Exceptions;

public class InvalidEmailException : Exception
{
    const string DEFAULT_INVALID_EMAIL_MESSAGE_TEMPLATE = "The Email Adress {0} is invalid";

    public InvalidEmailException(string Adress) : base(string.Format(DEFAULT_INVALID_EMAIL_MESSAGE_TEMPLATE, Adress))
    {
        
    }
}
