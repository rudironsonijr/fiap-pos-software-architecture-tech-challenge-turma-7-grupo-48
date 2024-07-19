using Domain.Entities.Enums;

namespace Controller.Dtos.PaymentRequest;

public record CreatePaymentRequest
{
	public int OrderId { get; init; }
	public PaymentMethodKind Kind { get; init; }
	public PaymentProvider Provider { get; init; }
}