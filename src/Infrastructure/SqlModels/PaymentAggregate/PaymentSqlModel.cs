using Domain.Entities.Enums;

namespace Infrastructure.SqlModels.PaymentAggregate;

public class PaymentSqlModel : BaseSqlModel
{
	public PaymentStatus Status { get; set; }
	public PaymentProvider? PaymentProvider { get; set; }
	public PaymentMethodKind? PaymentKind { get; set; }
	public required int OrderId { get; init; }
	public required string ExternalId { get; init; }
	public required decimal Amount { get; init; }
}
