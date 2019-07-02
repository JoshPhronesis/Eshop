using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetProductsAsync();
		Task<Product> GetProductAsync(int id);
		Task UpdateProductAsync(int id);
		Task AddProductAsync(Product product);
		Task DeleteProductAsync(int id);
	}
}
