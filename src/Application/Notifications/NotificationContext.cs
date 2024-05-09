using Domain.ValueObjects;

namespace Core.Notifications;

public class NotificationContext
{
	private readonly List<string> _errors;
	public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();
	public bool HasErrors => _errors.Any();

	public NotificationContext()
	{
		_errors = new List<string>();
	}

	public void AddNotification(string message)
	{
		_errors.Add(message);
	}

	public void AssertArgumentMinimumLength(decimal value, int minimum, string message)
	{
		if (value < minimum)
		{
			AddNotification(message);
		}
	}

	public void AssertArgumentNotNull(object? object1, string message)
	{
		if (object1 == null)
		{
			AddNotification(message);
		}
	}

	public void AssertArgumentInvalidCpf(string cpf)
	{
		if (cpf == null || !Cpf.IsValidCpf(cpf))
		{
			AddNotification($"The CPF: {cpf} is invalid");
		}
	}

	public void AssertArgumentInvalidEmail(string email)
	{
		if (email == null || !Email.IsValidEmail(email))
		{
			AddNotification($"The E-Mail: {email} is invalid");
		}
	}

	public void AssertArgumentNotNullOrWhiteSpace(string stringValue, string message)
	{
		if (!string.IsNullOrWhiteSpace(stringValue))
		{
			AddNotification(message);
		}
	}
}
