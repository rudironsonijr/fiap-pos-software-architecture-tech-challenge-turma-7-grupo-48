using Domain.Entities.Exceptions;

namespace Domain.Entities.Helpers
{
    internal static class EntityValidation
    {
        public static void FailIfNull(object? value, string propertyName)
        {
            EntityArgumentException.ThrowIfPropertyNull(value is null, propertyName, GetType());
        }

        public static void FailIfNullOrWhiteSpace(string? value, string propertyName)
        {
            EntityArgumentException.ThrowIfPropertyNull(string.IsNullOrWhiteSpace(value), propertyName, GetType());
        }

        public static void FailIfNullOrEmpty<T>(IEnumerable<T>? value, string propertyName)
        {
            var isNullOrEmpty = value is null || propertyName.Any() is false;
            EntityArgumentException.ThrowIfPropertyNull(isNullOrEmpty, propertyName, GetType());
        }
    }
}

