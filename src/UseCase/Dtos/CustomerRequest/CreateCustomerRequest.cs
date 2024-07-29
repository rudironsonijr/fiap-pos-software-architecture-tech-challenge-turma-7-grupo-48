namespace UseCase.Dtos.CustomerRequest;

public record CreateCustomerRequest
{
	public string Name { get; init; } = string.Empty;
	public string Email { get; init; } = string.Empty;
	public string Cpf { get; init; } = string.Empty;
}
