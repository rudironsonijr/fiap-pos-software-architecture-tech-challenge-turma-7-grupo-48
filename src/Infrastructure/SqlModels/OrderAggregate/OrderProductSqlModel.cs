using Infrastructure.SqlModels.ProductAggregate;

namespace Infrastructure.SqlModels.OrderAggregate;

public class OrderProductSqlModel : BaseSqlModel
{
	public int ProductId { get; set; }
	public virtual ProductSqlModel? Product { get; set; }
	public int OrderId { get; set; }
	public virtual OrderSqlModel? Order { get; set; } 
	public decimal ProductPrice { get; set; }
	public int Quantity { get; set; }
}
