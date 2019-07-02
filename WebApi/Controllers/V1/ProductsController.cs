using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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
        public async Task<IActionResult> Products()
        {
			var products = await productService.GetProductsAsync();
			var productsDto = mapper.Map<List<ProductDto>>(products);
			return Ok(productsDto);
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<IActionResult> Product(int productId)
		{
			if (productId <= 0)
			{
				return BadRequest();
			}

			var product = await productService.GetProductAsync(productId);
			var productDto = mapper.Map<ProductDto>(product);

			return Ok(productDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductDto productDto)
		{
			var product = mapper.Map<Product>(productDto);
			var prod = await productService.AddProductAsync(product);

			return CreatedAtAction("GetProduct", new { id = prod.Id }, productDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody]ProductDto product)
		{
			var productFromRepo = await productService.GetProductAsync(id);
			if (productFromRepo  == null)
			{
				return BadRequest();
			}

			var productFromDto = mapper.Map<Product>(product);
			await productService.UpdateProductAsync(productFromDto);

			return CreatedAtAction("GetProduct", new { id = productFromRepo.Id }, productFromDto);
		}
	}
}