using Domain.Entities.Enums;
using Domain.ValueObjects;

namespace UseCase.Services.Interfaces;

public interface IPaymentUseCase
{
	Task<Photo?> CreatePixAsync(int orderId, PaymentProvider provider, CancellationToken cancellationToken);

	Task ConfirmPaymentAsync(string externalId, CancellationToken cancellationToken);
}