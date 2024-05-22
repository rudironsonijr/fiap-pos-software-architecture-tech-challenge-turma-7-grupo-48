using Domain.Entities.Enums;

namespace Application.Dtos.PaymentRequest;

public record CreatePaymentRequest
{
	public int OrderId { get; init; }
	public PaymentMethodKind Kind { get; init; }
	public PaymentProvider Provider { get; init; }
}