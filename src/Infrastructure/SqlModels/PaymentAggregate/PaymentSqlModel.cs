using Domain.Entities.Enums;

namespace Infrastructure.SqlModels.PaymentAggregate;

public class PaymentSqlModel : BaseSqlModel
{
	public PaymentStatus Status { get; set; }
	public PaymentProvider PaymentProvider { get; set; }
	public PaymentMethodKind PaymentKind { get; set; }
	public required int OrderId { get; set; }
	public required string ExternalId { get; set; }
	public required decimal Amount { get; set; }
}
