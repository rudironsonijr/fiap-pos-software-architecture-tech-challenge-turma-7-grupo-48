using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

internal class EntityArgumentNullException(
	string propertyName,
	string entityName
) : DomainException(
	string.Format(
		DefaultEntityArgumentMessageTemplate,
		propertyName,
		entityName
	)
)
{
	private const string DefaultEntityArgumentMessageTemplate =
		"The property {0} can't be null or empty in model {1}";

	public static void ThrowIfPropertyNull(bool valid, string propertyName, Type entityType)
	{
		if (valid)
		{
			throw new EntityArgumentNullException(propertyName, entityType.ToString());
		}
	}

	public static void ThrowIfPropertyNull(object? value, string propertyName, Type entityType)
	{
		if (value == null)
		{
			throw new EntityArgumentNullException(propertyName, entityType.ToString());
		}
	}

	public static void ThrowIfNullOrWhiteSpace(string? value, string propertyName, Type entityType)
	{
		if (string.IsNullOrEmpty(value))
		{
			throw new EntityArgumentNullException(propertyName, entityType.ToString());
		}
	}

	public static void ThrowIfNullOrWhiteSpace<T>(IEnumerable<T>? value, string propertyName, Type entityType)
	{
		if (value is null || value.Any())
		{
			throw new EntityArgumentNullException(propertyName, entityType.ToString());
		}
	}
}