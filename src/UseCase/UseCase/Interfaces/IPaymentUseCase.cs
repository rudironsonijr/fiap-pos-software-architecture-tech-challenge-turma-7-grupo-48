using Domain.Entities.Enums;

namespace UseCase.Services.Interfaces;

public interface IPaymentUseCase
{
	Task<string?> CreatePixAsync(int orderId, PaymentProvider provider, CancellationToken cancellationToken);

	Task ConfirmPaymentAsync(int orderId, CancellationToken cancellationToken);
}