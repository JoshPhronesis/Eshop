using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Services
{
	[TestClass]
	public class ProductServicesTests
	{
		private Mock<IAsyncRepository<Product, int>> mockRepo;
		private IProductService service;
		IReadOnlyList<Product> products;

		[TestInitialize]
		public void Initialize()
		{
			mockRepo = new Mock<IAsyncRepository<Product, int>>();
			service = new ProductService(mockRepo.Object);
			products = new List<Product>()
			{
				new Product() { Id = 1, Name = "Iphone", Price=1000.00m, Description="good iphone" },
				new Product() { Id = 2, Name = "Samsung", Price=2000.00m, Description="good samsung" },
				new Product() { Id = 3, Name = "Motoroller", Price= 3000, Description="good motoroller" }
			};
		}

		[TestMethod]
		public void ProductService_GetProductsAsync_ShouldReturnListOfProducts()
		{
			//Arrange
			mockRepo.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(products));

			//Act
			List<Product> results = service.GetProductsAsync().Result as List<Product>;

			//Assert
			Assert.IsNotNull(results);
			Assert.AreEqual(3, results.Count);
		}

		[TestMethod]
		public  void ProductService_GetProductAsync_ShouldReturnProduct()
		{
			//Arrange
			int id = 1;
			mockRepo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(products[0]));

			//Act
			Product product = service.GetProductAsync(id).Result as Product;

			//Assert
			Assert.AreEqual(1, product.Id);
		}

		[TestMethod]
		public void ProductService_AddProductAsync_ShouldAddProduct()
		{
			//Arrange
			int Id = 4;
			Product prod = new Product() { Name = "Nokia" };
			mockRepo.Setup(m => m.AddAsync(prod)).Returns((Product p) =>
			{
				p.Id = Id;
				p.Name = "Nokia";
				return Task.FromResult(p);
			});

			//Act
			var resp = service.AddProductAsync(prod).Result;

			//Assert
			Assert.AreEqual(Id, resp.Id);
		}

		[TestMethod]
		public void ProductService_UpdateProductAsync_ShouldUpdateProduct()
		{
			//Arrange
			int Id = 1;
			string name = "Apple";
			var prod = products.FirstOrDefault(p => p.Id==1);
			prod.Name = name;
			mockRepo.Setup(m => m.AddAsync(prod)).Returns((Product p) =>
			{
				p.Id = Id;
				p.Name = name;
				return Task.FromResult(p);
			});

			//Act
			service.UpdateProductAsync(prod);

			//Assert
			Assert.AreEqual(Id, prod.Id);
			Assert.AreEqual("Apple", prod.Name);
		}
	}
}
