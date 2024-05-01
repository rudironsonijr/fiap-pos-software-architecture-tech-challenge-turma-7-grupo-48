using Domain.Entities.Exceptions;

namespace Domain.Entities.Helpers
{
	internal static class EntityValidation
	{
		public static void FailIfNull(object? value, string propertyName)
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				valid: value is null,
				propertyName: propertyName,
				entityType: value?.GetType()
			);
		}

		public static void FailIfNullOrWhiteSpace(string? value, string propertyName)
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				valid: string.IsNullOrWhiteSpace(value: value),
				propertyName: propertyName,
				entityType: value?.GetType()
			);
		}

		public static void FailIfNullOrEmpty<T>(IEnumerable<T>? value, string propertyName)
		{
			var isNullOrEmpty = value is null || !propertyName.Any();
			EntityArgumentNullException.ThrowIfPropertyNull(
				valid: isNullOrEmpty,
				propertyName: propertyName,
				entityType: value?.GetType()
			);
		}
	}
}