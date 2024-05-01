using Domain.Entities.Exceptions;

namespace Domain.Entities;

public abstract class EntityValidation
{
	protected void FailIfNull(
		object? value,
		string propertyName
	)
	{
		EntityArgumentException.ThrowIfPropertyNull(
			valid: value is null,
			propertyName: propertyName,
			entityType: GetType()
		);
	}

	protected void FailIfNullOrWhiteSpace(
		string? value,
		string propertyName
	)
	{
		EntityArgumentException.ThrowIfPropertyNull(
			valid: string.IsNullOrWhiteSpace(value: value),
			propertyName: propertyName,
			entityType: GetType()
		);
	}

	protected void FailIfNullOrEmpty<T>(
		IEnumerable<T>? value,
		string propertyName
	)
	{
		var isNullOrEmpty = value is null || !propertyName.Any();
		EntityArgumentException.ThrowIfPropertyNull(
			valid: isNullOrEmpty,
			propertyName: propertyName,
			entityType: GetType()
		);
	}
}