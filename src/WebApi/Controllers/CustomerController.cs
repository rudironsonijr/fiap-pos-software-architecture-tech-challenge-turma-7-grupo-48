using Controller.Application.Interfaces;
using Controller.Dtos.CustomerResponse;
using Microsoft.AspNetCore.Mvc;
using UseCase.Dtos.CustomerRequest;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{

	private readonly ICustomerApplication _customerService;
	public CustomerController(ICustomerApplication customerService)
	{
		_customerService = customerService;
	}

	[ProducesResponseType(typeof(IEnumerable<GetCustomerResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet]
	[Route("cpf/{cpf}")]
	public async Task<IActionResult> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
	{
		var response = await _customerService.GetByCpf(cpf, cancellationToken);

		if (response == null)
		{
			return NotFound();
		}

		return Ok(response);
	}

	[ProducesResponseType(typeof(IEnumerable<CreateCustomerResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public async Task<IActionResult> Create(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken)
	{
		var response = await _customerService.CreateAsync(createCustomerRequest, cancellationToken);

		return Ok(response);
	}
}