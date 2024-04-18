using Domain.ValueObjects;

namespace Domain.Entities;

public class Customer : EntityValidation
{
    public int Id { get; init; }

    private string _name = string.Empty;
    public required string Name { 
        get => _name; 
        init 
        { 
            FailIfNullOrWhiteSpace(value, nameof(Name));
            _name = value;
        } 
    }

    private Email _email;
    public required Email Email
    {
        get => _email;
        init
        {
            FailIfNull(value, nameof(Email));
            _email = value;
        }
    }

    private Cpf _cpf;
    public required Cpf Cpf
    {
        get => _cpf;
        init
        {
            FailIfNull(value, nameof(Email));
            _cpf = value;
        }
    }
}
