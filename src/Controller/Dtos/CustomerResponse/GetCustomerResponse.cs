namespace Controller.Dtos.CustomerResponse;

public record GetCustomerResponse
{	
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string Email { get; init; } = string.Empty;
	public string Cpf { get; init; } = string.Empty;
}
