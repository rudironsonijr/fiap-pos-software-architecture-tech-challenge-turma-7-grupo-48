using Controller.Dtos.PaymentRequest;

namespace Controller.Application.Interfaces;

public interface IPaymentApplication
{
	Task<string?> CreatePixAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken);
}
