namespace Domain.Entities.Exceptions;

public class EntityArgumentException(
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
			throw new EntityArgumentException(
				propertyName: propertyName,
				entityName: entityType.ToString()
			);
	}
}