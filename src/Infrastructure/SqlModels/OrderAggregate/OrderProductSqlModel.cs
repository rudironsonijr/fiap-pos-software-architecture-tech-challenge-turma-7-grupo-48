using Infrastructure.SqlModels.ProductAggregate;

namespace Infrastructure.SqlModels.OrderAggregate;

public class OrderProductSqlModel : BaseSqlModel
{
	public int ProductId { get; set; }
	public ProductSqlModel Product { get; set; } = new();
	public int OrderId { get; set; }
	public OrderSqlModel Order { get; set; } = new();
	public decimal ProductPrice { get; set; }
	public int Quantity { get; set; }
}
