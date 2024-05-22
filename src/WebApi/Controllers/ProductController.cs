using Application.Dtos.CustomerRequest;
using Application.Dtos.CustomerResponse;
using Application.Dtos.OrderResponse;
using Application.Dtos.ProductRequest;
using Application.Dtos.ProductResponse;
using Application.Services;
using Application.Services.Interfaces;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}


		[ProducesResponseType(typeof(IEnumerable<ProductGetResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
		{
			var response = await _productService.GetAsync(id, cancellationToken);

			if (response == null)
			{
				return NotFound();
			}

			return Ok(response);
		}

		[ProducesResponseType(typeof(IEnumerable<ProductCreateResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> CreateAsync(ProductCreateRequest productCreateRequest, CancellationToken cancellationToken)
		{
			var response = await _productService.CreateAsync(productCreateRequest, cancellationToken);

			return Ok(response);
		}

		[ProducesResponseType(typeof(IEnumerable<ProductCreateResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPatch]
		[Route("{id}/price")]
		public async Task<IActionResult> UpdatePriceAsync(int id, ProductUpdatePriceRequest productCreateRequest, CancellationToken cancellationToken)
		{
			await _productService.UpdatePriceAsync(id, productCreateRequest, cancellationToken);

			return NoContent();
		}

		[HttpPatch]
		[Route("{id}/image")]
		public async Task<IActionResult> AddImageAsync(int id, IFormFile file)
		{
			byte[] fileBytes;
			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				fileBytes = memoryStream.ToArray();
			}

			Photo photo = new Photo(file.FileName, file.ContentType, fileBytes);
			throw new NotImplementedException();
		}
	}
}
