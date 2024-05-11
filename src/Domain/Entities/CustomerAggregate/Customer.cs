using Domain.Entities.Base.Interfaces;
using Domain.Entities.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.CustomerAggregate;

public class Customer : IAggregateRoot
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
			EntityArgumentNullException.ThrowIfNullOrWhiteSpace(
				value,
				nameof(Name),
				GetType()
			);

			_name = value;
		}
	}

	public required Email Email
	{
		get => _email;
		init
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				value,
				nameof(Email),
				GetType()
			);

			_email = value;
		}
	}

	public required Cpf Cpf
	{
		get => _cpf;
		init
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				value,
				nameof(Email),
				GetType()
			);

			_cpf = value;
		}
	}
}