using UseCase.Dtos.PaymentRequest;
using UseCase.Dtos.PaymentResponse;

namespace UseCase.Services.Interfaces;

public interface IPaymentUseCase
{
	Task<CreatePaymentResponse?> CreateAsync(CreatePaymentRequest createPayment, CancellationToken cancellationToken);

	Task ConfirmPaymentAsync(int orderId, CancellationToken cancellationToken);
}