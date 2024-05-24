using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.SqlModels.CustomerAggregate;

public class CustomerSqlModel : BaseSqlModel
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Cpf { get; set; } = string.Empty;
	public IEnumerable<OrderSqlModel> Orders { get; set; } = Enumerable.Empty<OrderSqlModel>();
}