using Domain.Entities.Exceptions;

namespace Domain.Entities
{
    public abstract class EntityValidation
    {
        protected void FailIfNull(object? value, string propertyName)
        {
            EntityArgumentException.ThrowIfPropertyNull(value is null, propertyName, GetType());
        }

        protected void FailIfNullOrWhiteSpace(string? value, string propertyName)
        {
            EntityArgumentException.ThrowIfPropertyNull(string.IsNullOrWhiteSpace(value), propertyName, GetType());
        }

        protected void FailIfNullOrEmpty<T>(IEnumerable<T>? value, string propertyName)
        {
            var isNullOrEmpty = value is null || propertyName.Any() is false; 
            EntityArgumentException.ThrowIfPropertyNull(isNullOrEmpty, propertyName, GetType());
        }
    }
}

