using Domain.Exceptions;

namespace Domain.Entities.Base.Exceptions;

internal class EntityArgumentEnumInvalidException(
	string propertyName,
	string enumType,
	string entityName
	) : DomainException(
		message: string.Format(
			format: DefaultEntityEnumInvalidValuMessageTemplate,
			arg0: propertyName,
			arg1: enumType,
			arg2: entityName
		)
	)
{
	private const string DefaultEntityEnumInvalidValuMessageTemplate = "The property {0} can't be {1} in model {2}";
}
