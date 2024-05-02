using Application.Dtos.OrderRequest;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
	[HttpPost]
	public async Task<IActionResult> Create(
		OrderCreateRequest orderCreateRequest,
		CancellationToken cancellationToken)
	{
		var response = await _orderService.CreateAsync(orderCreateRequest, cancellationToken);

		return Ok(response);

	}

	[HttpPost]
	[Route("{id}/product")]
	public async Task<IActionResult> AddProduct(
		int id,
		OrderAddProductRequest orderAddProductRequest,
		CancellationToken cancellationToken)
	{
		var response = await _orderService.AddProduct(id, orderAddProductRequest, cancellationToken);

		return Ok(response);

	}

	[HttpPatch]
	[Route("{id}/order-product/{orderProductId}")]
	public async Task<IActionResult> UpdateProductQuantity(
		int id,
		int orderProductId,
		OrderUpdateProductQuantityRequest orderAddProductRequest,
		CancellationToken cancellationToken)
	{
		var response = await _orderService.UpdateProductQuantity(id, orderProductId, orderAddProductRequest, cancellationToken);

		return Ok(response);

	}

	[HttpDelete]
	[Route("{orderId}/order-product/{orderProductId}")]
	public async Task<IActionResult> RemoveProduct(
		int id,
		int orderProductId,
		CancellationToken cancellationToken)
	{
		var response = await _orderService.RemoveProduct(id, orderProductId, cancellationToken);

		return Ok(response);

	}

	[HttpDelete]
	[Route("{orderId}")]
	public async Task<IActionResult> CancelOrder(
		int id,
		int orderProductId,
		CancellationToken cancellationToken)
	{
		var response = await _orderService.RemoveProduct(id, orderProductId, cancellationToken);

		return NoContent();

	}
}
