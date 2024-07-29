using Controller.Application.Interfaces;
using Controller.Dtos.OrderResponse;
using Controller.Dtos.ProductResponse;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using UseCase.Dtos.ProductRequest;
using WebApi.Controllers.Exceptions;
using WebApi.DTOs;
using WebApi.DTOs.Extensions;
using WebApi.Extensions;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductApplication _productApplication;

		public ProductController(IProductApplication productApplication)
		{
			_productApplication = productApplication;
		}


		[ProducesResponseType(typeof(IEnumerable<ProductGetResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
		{
			var response = await _productApplication.GetAsync(id, cancellationToken);

			if (response == null)
			{
				return NotFound();
			}

			return Ok(response);
		}

		[ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet]
		[Route("{id}/Photo")]
		public async Task<FileResult> DownloadPhotoAsync(int id, CancellationToken cancellationToken)
		{
			var photo = await _productApplication.GetPhotoAsync(id, cancellationToken);

			if (photo == null)
			{
				throw new ControllerNotFoundException($"Photo for product Id: {id} not found");
			}

			return File(photo?.Data!, photo?.ContentType!);
		}

		[ProducesResponseType(typeof(IEnumerable<GetOrListOrderResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet]
		[Route("type/{productType}")]
		public async Task<IActionResult> ListAsync(ProductType productType, int? page, int? limit, CancellationToken cancellationToken)
		{
			var response = await _productApplication.ListAsync(productType, page, limit, cancellationToken);

			return Ok(response);
		}

		[ProducesResponseType(typeof(IEnumerable<ProductCreateResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> CreateAsync(ProductCreateWithFormFileRequest productCreateRequest, CancellationToken cancellationToken)
		{
			var request = await productCreateRequest.ToProductCreateRequestAsync(cancellationToken);
			var response = await _productApplication.CreateAsync(request, cancellationToken);

			return Ok(response);
		}

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPatch]
		[Route("{id}/price")]
		public async Task<IActionResult> UpdatePriceAsync(int id, ProductUpdatePriceRequest productCreateRequest, CancellationToken cancellationToken)
		{
			await _productApplication.UpdatePriceAsync(id, productCreateRequest, cancellationToken);

			return NoContent();
		}

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPatch]
		[Route("{id}/image")]
		public async Task<IActionResult> UpdateImageAsync(int id, IFormFile file, CancellationToken cancellationToken)
		{

			var photo = await file.ToPhotoAsync(cancellationToken);
			await _productApplication.UpdatePhoto(id, photo, cancellationToken);
			return NoContent();
		}
	}
}
