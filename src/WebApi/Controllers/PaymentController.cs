using Controller.Application.Interfaces;
using Controller.Dtos.PaymentRequest;
using Microsoft.AspNetCore.Mvc;
using UseCase.Services.Interfaces;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{

	private readonly IPaymentApplication _paymentApplication;
	public PaymentController(IPaymentApplication paymentService)
	{
		_paymentApplication = paymentService;
	}

	[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	[Route("pix")]
	public async Task<IActionResult> CreateAsync(CreatePaymentRequest paymentRequest, CancellationToken cancellationToken)
	{
		var response = await _paymentApplication.CreatePixAsync(paymentRequest, cancellationToken);

		return Ok(response);
	}
}