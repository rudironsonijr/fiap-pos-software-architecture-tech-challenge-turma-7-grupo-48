using Application.Dtos.PaymentRequest;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase {

	private readonly IPaymentService _paymentService;
	public PaymentController(IPaymentService paymentService)
	{
		_paymentService = paymentService;
	}

	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public async Task<IActionResult> CreatePayments(CreatePaymentRequest createPaymentRequest, CancellationToken cancellationToken)
	{
		var response = await _paymentService.CreatePaymentAsync(createPaymentRequest, cancellationToken);

		return Ok(response);
	}
}