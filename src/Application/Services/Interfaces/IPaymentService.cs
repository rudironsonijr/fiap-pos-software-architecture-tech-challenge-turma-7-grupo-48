using Application.Dtos.PaymentRequest;
using Application.Dtos.PaymentResponse;

namespace Application.Services.Interfaces;

public interface IPaymentService
{
	Task<CreatePaymentResponse?> CreateAsync(CreatePaymentRequest createPayment, CancellationToken cancellationToken);

	Task ConfirmPaymentAsync(int orderId, CancellationToken cancellationToken);
}