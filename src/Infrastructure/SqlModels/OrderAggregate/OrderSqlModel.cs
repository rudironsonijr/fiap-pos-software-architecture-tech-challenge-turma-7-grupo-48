using Domain.Entities.Enums;
using Domain.ValueObjects;
using Infrastructure.SqlModels.CustomerAggregate;

namespace Infrastructure.SqlModels.OrderAggregate;

public class OrderSqlModel : BaseSqlModel
{
	public int CustomerId { get; set; }
	public CustomerSqlModel? Customer { get; set; }
	public OrderStatus Status { get; set; }
	public PaymentProvider? PaymentProvider { get; set; }
	public PaymentMethodKind? PaymentKind { get; set; }
	public List<OrderProductSqlModel> OrderProducts { get; set; } = [];
	public decimal Price { get; set; }
}
