using Application.Dtos.PaymentRequest;
using Application.Dtos.PaymentResponse;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{

	private readonly IPaymentService _paymentService;
	public PaymentController(IPaymentService paymentService)
	{
		_paymentService = paymentService;
	}

	[ProducesResponseType(typeof(CreatePaymentResponse),StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public async Task<IActionResult> CreateAsync(CreatePaymentRequest paymentRequest, CancellationToken cancellationToken)
	{
		var response = await _paymentService.CreateAsync(paymentRequest, cancellationToken);

		return Ok(response);
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPatch]
	[Route("confirm/order/{orderId}")]
	public async Task<IActionResult> ConfirmPaymentAsync(int orderId, CancellationToken cancellationToken)
	{
		await _paymentService.ConfirmPaymentAsync(orderId, cancellationToken);

		return NoContent();
	}
}