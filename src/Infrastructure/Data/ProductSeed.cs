using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
	public class ProductSeed
	{
		private readonly DataContext context;
		public ProductSeed(DataContext context)
		{
			this.context = context;
		}

		public async static Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
		{
			try
			{
				var data = System.IO.File.ReadAllText("seedData.json");
				var seedData = JsonConvert.DeserializeObject<List<Product>>(data);

				if (!context.Products.Any())
				{
					await context.Products.AddRangeAsync(seedData);
					await context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<ProductSeed>();
				logger.LogError(ex, "An error occurred while seeding.");
			}
			
		}
	}
}
