using Adapters.Webhook.Dtos;
using Controller.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Webhooks;

[Route("api/webhook/[controller]")]
[ApiController]
public class MercadoPagoController : ControllerBase
{
	private readonly IMercadoPagoApplication _mercadoPagoApplication;
	public MercadoPagoController(IMercadoPagoApplication mercadoPagoApplication)
	{
		_mercadoPagoApplication = mercadoPagoApplication;
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public async Task<IActionResult> Post(MercadoPagoEventRequest mercadoPagoEvent, CancellationToken cancellationToken)
	{
		await _mercadoPagoApplication.ProcessEvent(mercadoPagoEvent, cancellationToken);
		return Ok();
	}
}
