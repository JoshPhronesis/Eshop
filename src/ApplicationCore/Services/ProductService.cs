using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class ProductService : IProductService
	{
		private readonly IAsyncRepository<Product, int> repo;

		public ProductService(IAsyncRepository<Product, int> repository)
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

		public Task<PagedList<Product>> GetPagedProducts(int pageNumber, int pageSize, decimal minPrice, decimal MaxPrice, string orderBy, string searchTerm)
		{
			var query = repo.GetAllAsQueryable().Result.Where(p => p.Price > minPrice && p.Price < MaxPrice);
			if (!string.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
			}
			return Task.FromResult(PagedList<Product>.CreateAsync(query, pageNumber, pageSize, nameof(Product.Price), orderBy));
		}

		public async Task UpdateProductAsync(Product product)
		{
			if (product == null)
			{
				throw new ProductNotFoundException("Product cannot be null");
			}

			await repo.UpdateAsync(product);
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			return await repo.GetAllAsync();
		}
	}
}
