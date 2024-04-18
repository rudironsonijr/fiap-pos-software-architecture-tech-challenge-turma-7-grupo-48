namespace Domain.Entities.Exceptions;

public class EntityArgumentException : Exception
{
    const string DEFAULT_ENTITY_ARGUMENT_MESSAGE_TEMPLATE = "The property {0} can't be null or empty in model {1}";

    public EntityArgumentException(string propertyName, string entityName) : base(string.Format(DEFAULT_ENTITY_ARGUMENT_MESSAGE_TEMPLATE, propertyName, entityName)) { }
    public static void ThrowIfPropertyNull(bool valid, string propertyName, Type entityType)
    {
        if (valid)
        {
            throw new EntityArgumentException(propertyName, entityType.ToString());
        }
    }
}
