using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.SwaggerDoc("v1", new Info()
				{
					Title = "Eshop API",
					Version = "v1",
					Description = "Eshop service HTTP API",
					TermsOfService = "Terms Of Service"
				});
			});

			services.AddCors();
			services.AddAutoMapper(typeof(Startup));
			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
			services.AddScoped<IProductService, ProductService>();
			services.AddDbContext<DataContext>(options => 
			{
				options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Infrastructure"));
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eshop service API V1");
			});

			app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
