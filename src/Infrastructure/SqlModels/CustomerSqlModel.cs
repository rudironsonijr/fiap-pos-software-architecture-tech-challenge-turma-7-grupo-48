namespace Infrastructure.SqlModels;

public class CustomerSqlModel : BaseSqlModel
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Cpf { get; set; } = string.Empty;
}