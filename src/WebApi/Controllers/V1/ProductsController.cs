using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Helpers;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
		private readonly IMapper mapper;
		private readonly IProductService productService;

		public ProductsController(IMapper mapper, IProductService productService)
		{
			this.mapper = mapper;
			this.productService = productService;
		}

		[HttpGet]
		public IActionResult Products([FromQuery]PaginationParams pagination)
		{
			var products = productService.GetPagedProducts(pagination.PageNumber, pagination.PageSize);
			var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
			Response.AddPagination(pagination.PageNumber, pagination.PageSize, products.TotalCount, products.TotalPages);

			return Ok(productsDto);
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<IActionResult> Product(int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			var product = await productService.GetProductAsync(id);
			var productDto = mapper.Map<ProductDto>(product);

			return Ok(productDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductDto productDto)
		{
			var product = mapper.Map<Product>(productDto);
			var prod = await productService.AddProductAsync(product);

			return CreatedAtRoute("GetProduct", new { id = prod?.Id }, productDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody]ProductDto product)
		{
			var productFromRepo = await productService.GetProductAsync(id);
			if (productFromRepo == null)
			{
				return BadRequest();
			}

			product.Id = id;
			mapper.Map(product, productFromRepo);

			await productService.UpdateProductAsync(productFromRepo);

			var productToReturn = mapper.Map<ProductDto>(productFromRepo);
			return CreatedAtRoute("GetProduct", new { id = productFromRepo.Id }, productToReturn);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var productFromRepo = await productService.GetProductAsync(id);
			if (productFromRepo == null)
			{
				return BadRequest();
			}

			await productService.DeleteProductAsync(id);

			return Ok();
		}
	}
}