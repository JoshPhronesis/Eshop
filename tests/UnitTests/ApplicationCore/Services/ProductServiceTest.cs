using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Services
{

	public class ProductServiceTest
	{
		private Mock<IAsyncRepository<Product, int>> mockRepo;

		public ProductServiceTest()
		{
			mockRepo = new Mock<IAsyncRepository<Product, int>>();
		}

		public async Task AddProduct_AddProductToDb_ProductShouldBeAdded()
		{
	
		}
	}
}
