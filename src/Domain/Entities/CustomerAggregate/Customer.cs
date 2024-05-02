using Domain.Entities.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.CustomerAggregate;

public class Customer
{
	public int Id { get; init; }

	private string _name = string.Empty;
	public required string Name
	{
		get => _name;
		init
		{
			EntityArgumentNullException.ThrowIfNullOrWhiteSpace(
				value, 
				nameof(Name), 
				GetType());

			_name = value;
		}
	}

	private Email _email;
	public required Email Email
	{
		get => _email;
		init
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				value, 
				nameof(Email), 
				GetType());

			_email = value;
		}
	}

	private Cpf _cpf;
	public required Cpf Cpf
	{
		get => _cpf;
		init
		{
			EntityArgumentNullException.ThrowIfPropertyNull(
				value, 
				nameof(Email), 
				GetType());

			_cpf = value;
		}
	}
}