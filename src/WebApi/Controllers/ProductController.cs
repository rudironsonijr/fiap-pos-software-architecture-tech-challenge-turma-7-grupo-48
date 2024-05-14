using Application.Dtos.CustomerRequest;
using Application.Dtos.CustomerResponse;
using Application.Dtos.ProductRequest;
using Application.Dtos.ProductResponse;
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

		[ProducesResponseType(typeof(IEnumerable<ProductCreateResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Create(ProductCreateRequest productCreateRequest, CancellationToken cancellationToken)
		{
			var response = await _productService.CreateAsync(productCreateRequest, cancellationToken);

			return Ok(response);
		}

		[HttpPatch]
		[Route("{id}/image")]
		public async Task<IActionResult> AddImage(int id, IFormFile file)
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
