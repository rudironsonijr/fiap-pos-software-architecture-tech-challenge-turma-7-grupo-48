using Application.Dtos.CustomerRequest;
using Application.Dtos.CustomerResponse;
using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase {

	private readonly ICustomerService _customerService;
	public CustomerController(ICustomerService customerService)
	{
		_customerService = customerService;
	}

	[ProducesResponseType(typeof(IEnumerable<GetOrderResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet]
	[Route("cpf/{cpf}")]
	public async Task<IActionResult> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
	{
		var response = await _customerService.GetByCpf(cpf, cancellationToken);

		if(response == null)
		{
			return NotFound();
		}

		return Ok(response);
	}

	[ProducesResponseType(typeof(IEnumerable<CreateCustomerResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public async Task<IActionResult> Create(CreateCustomerRequest CreateCustomerRequest, CancellationToken cancellationToken)
	{
		var response = await _customerService.CreateAsync(CreateCustomerRequest, cancellationToken);

		return Ok(response);
	}
}