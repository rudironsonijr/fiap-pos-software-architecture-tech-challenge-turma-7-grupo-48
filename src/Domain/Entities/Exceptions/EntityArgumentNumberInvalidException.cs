using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

internal class EntityArgumentNumberInvalidException : DomainException
{
	private const string DefaultNumberInvalidGreaterOrEqualMessageTemplate =
		"The property {0} can't be greater than or equal {1} in model {2}";

	private const string DefaultNumberInvalidLessMessageTemplate =
		"The property {0} can't be less than or equal {1} in model {2}";

	private EntityArgumentNumberInvalidException(
		string message,
		string propertyName,
		decimal defaultValue,
		string entityName
	)
		: base(
			string.Format(
				message,
				propertyName,
				defaultValue,
				entityName
			)
		)
	{ }

	public static void ThrowIfLessOrEqualZero(
		decimal compareValue,
		string propertyName,
		Type entityType
	)
	{
		if (compareValue <= 0)
			throw new EntityArgumentNumberInvalidException(
				DefaultNumberInvalidLessMessageTemplate,
				propertyName,
				0,
				entityType.ToString()
			);
	}
}