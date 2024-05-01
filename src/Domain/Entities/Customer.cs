using Domain.ValueObjects;

namespace Domain.Entities;

public class Customer : EntityValidation
{
	private readonly Cpf _cpf;

	private readonly Email _email;

	private readonly string _name = string.Empty;
	public int Id { get; init; }

	public required string Name
	{
		get => _name;
		init
		{
			FailIfNullOrWhiteSpace(
				value: value,
				propertyName: nameof(Name)
			);
			_name = value;
		}
	}

	public required Email Email
	{
		get => _email;
		init
		{
			FailIfNull(
				value: value,
				propertyName: nameof(Email)
			);
			_email = value;
		}
	}

	public required Cpf Cpf
	{
		get => _cpf;
		init
		{
			FailIfNull(
				value: value,
				propertyName: nameof(Email)
			);
			_cpf = value;
		}
	}
}