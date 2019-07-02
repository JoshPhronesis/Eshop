using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var loggerFactory = services.GetRequiredService<ILoggerFactory>();
				try
				{
					var context = services.GetRequiredService<DataContext>();
					await ProductSeed.SeedAsync(context, loggerFactory);
				}
				catch (Exception ex)
				{
					var logger = loggerFactory.CreateLogger<Program>();
					logger.LogError(ex, "An error occurred while seeding.");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
