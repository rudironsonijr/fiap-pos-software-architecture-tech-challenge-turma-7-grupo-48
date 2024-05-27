using Application.Dtos.OrderRequest;
using Application.Dtos.OrderResponse;
using Application.Services.Interfaces;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
	private readonly IOrderService _orderService;

	public OrderController(IOrderService orderService)
	{
		_orderService = orderService;
	}

	[ProducesResponseType(typeof(GetOrListOrderResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet]
	[Route("{id}")]
	public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
	{
		var response = await _orderService.GetAsync(id, cancellationToken);

		if (response == null)
		{
			return NotFound();
		}

		return Ok(response);
	}

	[ProducesResponseType(typeof(IEnumerable<GetOrListOrderResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet]
	[Route("status/{orderStatus}")]
	public async Task<IActionResult> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken)
	{
		var response = await _orderService.ListAsync(orderStatus, page, limit, cancellationToken);

		return Ok(response);
	}

	[ProducesResponseType(typeof(IEnumerable<CreateOrderResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public async Task<IActionResult> Create(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken)
	{
		var response = await _orderService.CreateAsync(orderCreateRequest, cancellationToken);

		return Ok(response);
	}

	[ProducesResponseType(typeof(IEnumerable<OrderUpdateOrderProductResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	[Route("{id}/product")]
	public async Task<IActionResult> AddProductAsync(
		int id,
		OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken)
	{
		var response = await _orderService.AddProduct(id, orderAddProductRequest, cancellationToken);

		return Ok(response);
	}


	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPatch]
	[Route("{id}/status/Preparing")]
	public async Task<IActionResult> UpdateStatusToPreparing(int id, CancellationToken cancellationToken)
	{
		await _orderService.UpdateStatusToPreparing(id, cancellationToken);

		return NoContent();
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPatch]
	[Route("{id}/status/done")]
	public async Task<IActionResult> UpdateStatusToDone(int id, CancellationToken cancellationToken)
	{
		await _orderService.UpdateStatusToDone(id, cancellationToken);

		return NoContent();
	}


	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPatch]
	[Route("{id}/status/finished")]
	public async Task<IActionResult> UpdateStatusToFinished(int id, CancellationToken cancellationToken)
	{
		await _orderService.UpdateStatusToFinished(id, cancellationToken);

		return NoContent();
	}

	[ProducesResponseType(typeof(IEnumerable<OrderUpdateOrderProductResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpDelete]
	[Route("{orderId}/product/{productId}")]
	public async Task<IActionResult> RemoveProductAsync(int orderId, int productId, CancellationToken cancellationToken)
	{
		var response = await _orderService.RemoveProduct(orderId, productId, cancellationToken);
		return Ok(response);
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpDelete]
	[Route("{orderId}")]
	public async Task<IActionResult> CancelOrderAsync(int orderId, CancellationToken cancellationToken)
	{
		await _orderService.CancelOrder(orderId, cancellationToken);

		return NoContent();
	}
}