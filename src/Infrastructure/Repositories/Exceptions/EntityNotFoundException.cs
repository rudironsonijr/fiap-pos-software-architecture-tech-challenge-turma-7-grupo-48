namespace Infrastructure.Exceptions;

public class EntityNotFoundException : Exception
{
	private const string DEFAULT_MESSAGE = "The {1} with {2}: {3} not found";
	private EntityNotFoundException(string entityName, string parameter, string parameterValue)
		: base(string.Format(DEFAULT_MESSAGE, entityName, parameter, parameterValue))
	{

	}

	public static void ThrowIfPropertyNull(object? value, Type entityType, string parameter, string parameterValue)
	{
		if (value == null)
		{
			throw new EntityNotFoundException(entityType.ToString(), parameter, parameterValue);
		}
	}

	public static void ThrowIfPropertyNull(object? value, Type entityType, string parameter, int parameterValue)
	{
		if (value == null)
		{
			throw new EntityNotFoundException(entityType.ToString(), parameter, parameterValue.ToString());
		}
	}
}
