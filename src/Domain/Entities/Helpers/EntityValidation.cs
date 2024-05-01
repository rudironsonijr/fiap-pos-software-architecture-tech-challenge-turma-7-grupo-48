using Domain.Entities.Exceptions;

namespace Domain.Entities.Helpers
{
	internal static class EntityValidation
	{
		public static void FailIfNull(object? value, string propertyName, Type entityType)
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				value is null,
				propertyName,
				entityType
			);
		}

		public static void FailIfNullOrWhiteSpace(string? value, string propertyName, Type entityType)
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				string.IsNullOrWhiteSpace(value),
				propertyName,
				entityType
			);
		}

		public static void FailIfNullOrEmpty<T>(IEnumerable<T>? value, string propertyName, Type entityType)
		{
			var isNullOrEmpty = value is null || !propertyName.Any();
			EntityArgumentNullException.ThrowIfPropertyNull(
				isNullOrEmpty,
				propertyName,
				entityType
			);
		}

		public static void FailIfLessOrEqualZero(decimal value,  string propertyName, Type entityType)
		{
			EntityArgumentNumberInvalidException.ThrowIfLessOrEqualThan(
					0, 
					value,
					propertyName,
					entityType
				);
		}
	}
}