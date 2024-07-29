using Controller.Application.Interfaces;
using Controller.Dtos.PaymentRequest;
using Microsoft.AspNetCore.Mvc;

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

	[ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	[Route("pix")]
	public async Task<IActionResult> CreatePixAsync(CreatePaymentRequest paymentRequest, CancellationToken cancellationToken)
	{
		var response = await _paymentApplication.CreatePixAsync(paymentRequest, cancellationToken);

		return File(response?.Data!, response?.ContentType!);
	}
}