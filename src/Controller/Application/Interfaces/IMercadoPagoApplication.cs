using Adapters.Webhook.Dtos;

namespace Controller.Application.Interfaces;

public interface IMercadoPagoApplication
{
	Task ProcessEvent(MercadoPagoEventRequest mercadoPagoEvent, CancellationToken cancellationToken);
}
