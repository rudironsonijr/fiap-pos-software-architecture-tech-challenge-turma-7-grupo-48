namespace Controller.Dtos.PaymentResponse;

public record CreatePaymentResponse
{
	public string PaymentId { get; init; } = string.Empty;
}