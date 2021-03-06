﻿using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetProductsAsync();
		Task<PagedList<Product>> GetPagedProducts(int pageNumber, int pageSize, decimal minPrice, decimal maxPrice, string orderBy, string searchTerm);
		Task<Product> GetProductAsync(int id);
		Task UpdateProductAsync(Product product);
		Task<Product> AddProductAsync(Product product);
		Task DeleteProductAsync(int id);
	}
}
