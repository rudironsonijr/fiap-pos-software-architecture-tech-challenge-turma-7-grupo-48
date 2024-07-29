using Controller.Application.Interfaces;
using Controller.Dtos.PaymentRequest;
using Domain.ValueObjects;
using UseCase.Services.Interfaces;

namespace Controller.Application;

public class PaymentApplication : IPaymentApplication
{
	private readonly IPaymentUseCase _paymentUseCase;
	public PaymentApplication(IPaymentUseCase paymentUseCase)
	{
		_paymentUseCase = paymentUseCase;
	}
	public Task<Photo?> CreatePixAsync(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken)
	{
		return
			 _paymentUseCase.CreatePixAsync(createPaymentRequest.OrderId, createPaymentRequest.paymentProvider, cancellationToken);
	}
}
