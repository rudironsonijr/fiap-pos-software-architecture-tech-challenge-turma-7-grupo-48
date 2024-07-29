using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

internal class EntityArgumentEnumInvalidException(string propertyName, string enumType, string entityName) :
	DomainException(string.Format(DefaultEntityEnumInvalidValuMessageTemplate, propertyName, enumType, entityName))
{
	private const string DefaultEntityEnumInvalidValuMessageTemplate = "The property {0} can't be {1} in model {2}";
}