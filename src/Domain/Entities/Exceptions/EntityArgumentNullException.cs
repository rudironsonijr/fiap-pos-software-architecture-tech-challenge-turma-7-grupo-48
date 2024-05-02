namespace Domain.Entities.Exceptions;

public class EntityArgumentNullException(
	string propertyName,
	string entityName
) : Exception(
	message: string.Format(
		format: DefaultEntityArgumentMessageTemplate,
		arg0: propertyName,
		arg1: entityName
	)
)
{
	private const string DefaultEntityArgumentMessageTemplate =
		"The property {0} can't be null or empty in model {1}";

	public static void ThrowIfPropertyNull(
		bool valid,
		string propertyName,
		Type entityType
	)
	{
		if (valid)
			throw new EntityArgumentNullException(
				propertyName,
				entityType.ToString()
			);
	}

	public static void ThrowIfPropertyNull(object? value, string propertyName, Type entityType)
	{
		if(value == null)
			throw new EntityArgumentNullException(
					propertyName,
					entityType.ToString()
				);
	}

	public static void ThrowIfNullOrWhiteSpace(string? value, string propertyName, Type entityType)
	{
		if (string.IsNullOrEmpty(value))
			throw new EntityArgumentNullException(
					propertyName,
					entityType.ToString()
				);
	}

}