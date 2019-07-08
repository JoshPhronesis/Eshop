using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DTOs;
using WebApi.Helpers;
using MockQueryable;

namespace UnitTests.WebApi
{

	[TestClass]
	public class ProductControllerTests
	{
		private Mock<IAsyncRepository<Product, int>> mockRepo;
		private Mock<IProductService> productService;
		private Mock<UserParams> paginationParams;

		ProductsController controller;
		IEnumerable<Product> products;
		PagedList<Product> pagedProducts;

		[TestInitialize]
		public void Initialize()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutomapperProfile());
			});

			var mapper = mockMapper.CreateMapper();
			productService = new Mock<IProductService>();

			paginationParams = new Mock<UserParams>();
			controller = new ProductsController(mapper, productService.Object);

			products = new List<Product>()
			{
				new Product() { Id = 1, Name = "Iphone", Price=1000.00m, Description="good iphone" },
				new Product() { Id = 2, Name = "Samsung", Price=2000.00m, Description="good samsung" },
				new Product() { Id = 3, Name = "Motoroller", Price= 3000, Description="good motoroller" }
			};

			pagedProducts = PagedList<Product>.CreateAsync(products.AsQueryable(), 1, 2, nameof(Product.Price), "mostExpensive");

		}

		[TestMethod]
		public void GetAll_Products_ReturnsAllProduct()
		{
			//arrange
			paginationParams.Object.PageSize = 2;
			paginationParams.Object.PageNumber = 1;
			paginationParams.Object.MaxPrice = 100000;
			paginationParams.Object.MinPrice = 1;
			productService.Setup(x => x.GetPagedProducts(paginationParams.Object.PageNumber, paginationParams.Object.PageSize,
											paginationParams.Object.MinPrice, paginationParams.Object.MaxPrice, paginationParams.Object.OrderBy, paginationParams.Object.SearchTerm))
						  .Returns(Task.FromResult(pagedProducts));

			//act
			var response = controller.Products(paginationParams.Object) as ObjectResult;
			var value = (List<ProductDto>)response.Value;

			//assert
			Assert.IsNotNull(value);
			Assert.AreEqual(2, value.Count);
			Assert.AreEqual(value[0].Price, pagedProducts.Max(p => p.Price));
		}

		[TestMethod]
		public void Get_ProductById_ReturnsProductWithSameId()
		{
			// Arrange
			int id = 1;
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));

			// Act
			IActionResult actionResult = controller.Product(id).Result;
			var contentResult = actionResult as ObjectResult;
			var value = (ProductDto)contentResult.Value;

			// Assert
			Assert.IsNotNull(value);
			Assert.AreEqual(1, value.Id);
		}

		[TestMethod]
		public void Get_ProductByInvalidId_ReturnsNullForProductAndStatusCode200()
		{
			// Arrange
			int id = 5;
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));

			// Act
			var contentResult = controller.Product(id).Result as ObjectResult;
			var value = (ProductDto)contentResult.Value;

			// Assert
			Assert.IsNull(value);
			Assert.AreEqual(200, contentResult.StatusCode);
		}

		[TestMethod]
		public void Get_ProductByZeroId_ReturnsStatusCode400()
		{
			// Arrange
			int id = 0;
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));

			// Act
			var badRequestResult = controller.Product(id).Result as BadRequestResult;

			// Assert
			Assert.IsNotNull(badRequestResult);
			Assert.AreEqual(400, badRequestResult.StatusCode);
		}

		[TestMethod]
		public void Delete_ProductWithInvalidId_ReturnsStatusCode400()
		{
			// Arrange
			int id = 5;
			var prodToRemoveIndex = products.ToList().FindIndex(p => p.Id == id);
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));

			// Act
			var badRequestResult = controller.Delete(id).Result as BadRequestResult;

			// Assert
			Assert.IsNotNull(badRequestResult);
			Assert.AreEqual(400, badRequestResult.StatusCode);
			Assert.AreEqual(3, products.ToList().Count);
		}

		[TestMethod]
		public void Delete_Product_ProductIsDeleted()
		{
			// Arrange
			int id = 1;
			var prodToRemoveIndex = products.ToList().FindIndex(p => p.Id == id);
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));
			productService.Setup(x => x.DeleteProductAsync(id)).Callback(() => { products = products.Where(p => p.Id != id);});

			// Act
			IActionResult actionResult = controller.Delete(id).Result;

			// Assert
			Assert.IsInstanceOfType(actionResult, typeof(OkResult));
			Assert.IsTrue(!products.Any(p => p.Id == id));
			Assert.AreEqual(2, products.ToList().Count);
		}

		[TestMethod]
		public void Update_ProductWithInvalidId_ReturnsErrorCode400()
		{
			// Arrange
			int id = 5;
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));

			// Act
			var badRequestResult = controller.Update(id, new ProductDto()).Result as BadRequestResult;

			// Assert
			Assert.IsNotNull(badRequestResult);
			Assert.AreEqual(400, badRequestResult.StatusCode);
			Assert.AreEqual(3, products.ToList().Count);
		}

		[TestMethod]
		public void Update_ProductUpdate_ProductIsUpdated()
		{
			// Arrange
			int id = 1;
			var product= products.First(p => p.Id == id);
			productService.Setup(x => x.GetProductAsync(id)).Returns(Task.FromResult(products.FirstOrDefault(p => p.Id == id)));
			productService.Setup(x => x.UpdateProductAsync(product)).Callback(() => { products.First(p => p.Id == id).Name = "Updated Product Name"; });

			// Act
			var result = controller.Update(id, new ProductDto ()).Result as CreatedAtRouteResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(products.First(p=>p.Id==id).Name, "Updated Product Name");
			Assert.AreEqual(201, result.StatusCode);
		}

		[TestMethod]
		public void Create_Product_ProductIsCreated()
		{
			// Arrange
			var newProduct = new Product() { Id = 4, Name = "New Product" };
			var newList = new List<Product>();
			newList.AddRange(products);
			newList.Add(newProduct);
			productService.Setup(x => x.AddProductAsync(newProduct));

			// Act
			var result = controller.Create(new ProductDto()).Result as CreatedAtRouteResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(4, newList.Last().Id);
			Assert.AreEqual("New Product", newList.Last().Name);
			Assert.AreEqual(201, result.StatusCode);
			Assert.AreEqual(4, newList.ToList().Count);
		}
	}
}
