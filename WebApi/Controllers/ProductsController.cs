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
		private readonly IRepository<Product, int> repository;
		private readonly IMapper mapper;

		public ProductsController(IRepository<Product,int> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet]
        public async Task<IActionResult> Products()
        {
			var products = await repository.GetAllAsync();
			var productsDto = mapper.Map<List<ProductToReturnDto>>(products);
			return Ok(productsDto);
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<IActionResult> Product(int productId)
		{
			if (productId <= 0)
			{
				return BadRequest();
			}

			var product = await repository.GetByIdAsync(productId);
			var productDto = mapper.Map<ProductToReturnDto>(product);

			return Ok(productDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductToReturnDto productDto)
		{
			var product = mapper.Map<Product>(productDto);
			var prod = await repository.AddAsync(product);

			return CreatedAtAction("GetProduct", new { id = prod.Id }, productDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody]ProductToReturnDto product)
		{
			var productFromRepo = await repository.GetByIdAsync(id);
			if (productFromRepo  == null)
			{
				return BadRequest();
			}

			var productFromDto = mapper.Map<Product>(product);
			await repository.UpdateAsync(productFromDto);

			return CreatedAtAction("GetProduct", new { id = productFromRepo.Id }, productFromDto);
		}
	}
}