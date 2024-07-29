using Adapters.Webhook.Dtos;
using Controller.Application.Interfaces;
using UseCase.Services.Interfaces;

namespace Integration.Controller.Interfaces;

public class MercadoPagoApplication : IMercadoPagoApplication
{
	private const string UPDATE_ACTION_VAlUE = "payment.updated";

	private readonly IPaymentUseCase _paymentUseCase;
	public MercadoPagoApplication(IPaymentUseCase paymentUseCase)
	{
		_paymentUseCase = paymentUseCase;
	}

	public Task ProcessEvent(MercadoPagoEventRequest mercadoPagoEvent, CancellationToken cancellationToken)
	{
		if (mercadoPagoEvent.Action != UPDATE_ACTION_VAlUE)
		{
			return Task.CompletedTask;
		}

		return _paymentUseCase.ConfirmPaymentAsync(mercadoPagoEvent.Data.Id, cancellationToken);

	}
}
