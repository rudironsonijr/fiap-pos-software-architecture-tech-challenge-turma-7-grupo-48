using Domain.Entities.Enums;
using Infrastructure.SqlModels.OrderAggregate;

namespace Infrastructure.SqlModels.ProductAggregate;

public class ProductSqlModel : BaseSqlModel
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public ProductType ProductType { get; set; }
	public decimal Price { get; set; }
	public string? PhotoFilename { get; set; }
	public string? PhotoContentType { get; set; }
	public byte[]? PhotoData { get; set; }

	public virtual List<OrderProductSqlModel> OrderProducts { get; set; } = new();

}
