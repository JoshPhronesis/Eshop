using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class ProductService : IProductService
	{
		private readonly IRepository<Product, int> repo;

		public ProductService(IRepository<Product, int> repository)
		{
			this.repo = repository;
		}

		public async Task<Product> AddProductAsync(Product product)
		{
			return await repo.AddAsync(product);
		}

		public async Task DeleteProductAsync(int id)
		{
			var product = await repo.GetByIdAsync(id);
			if (product == null)
			{
				throw new ProductNotFoundException(id);
			}

			await repo.DeleteAsync(product);
		}

		public async Task<Product> GetProductAsync(int productId)
		{
			return await repo.GetByIdAsync(productId);
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			return await repo.GetAllAsync();
		}

		public async Task UpdateProductAsync(Product product)
		{
			if (product == null)
			{
				throw new ProductNotFoundException("Product cannot be null");
			}

			await repo.UpdateAsync(product);
		}
	}
}
