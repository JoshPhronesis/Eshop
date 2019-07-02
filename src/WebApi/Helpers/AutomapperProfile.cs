using ApplicationCore.Entities;
using AutoMapper;
using WebApi.DTOs;

namespace WebApi.Helpers
{
	public class AutomapperProfile:Profile
	{
		public AutomapperProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}
