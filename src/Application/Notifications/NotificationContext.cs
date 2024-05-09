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

	public void AssertArgumentNotNull(object object1, string message)
	{
		if (object1 == null)
		{
			AddNotification(message);
		}
	}
}
