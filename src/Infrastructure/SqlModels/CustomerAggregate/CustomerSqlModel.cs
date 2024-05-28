using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.SqlModels.CustomerAggregate;

public class CustomerSqlModel : BaseSqlModel
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Cpf { get; set; } = string.Empty;
	public virtual List<OrderSqlModel> Orders { get; set; } = new();
}