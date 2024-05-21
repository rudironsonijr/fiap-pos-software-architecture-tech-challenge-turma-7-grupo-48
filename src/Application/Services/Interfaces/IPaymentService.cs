using Application.Dtos.PaymentRequest;

namespace Application.Services.Interfaces;

public interface IPaymentService
{
	Task<bool> CreatePaymentAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken);
}