using Domain.Entities.Enums;

namespace Controller.Dtos.PaymentRequest;

public record CreatePaymentRequest
{
	public int OrderId { get; set; } 
	public PaymentProvider paymentProvider { get; set; }
}
