using Controller.Dtos.PaymentRequest;
using Domain.ValueObjects;

namespace Controller.Application.Interfaces;

public interface IPaymentApplication
{
	Task<Photo?> CreatePixAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken);
}
