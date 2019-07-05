using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
	public static class Extensions
	{
		public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			if (response != null)
			{
				var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
				var camelCaseFormatter = new JsonSerializerSettings();
				camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
				response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
				response.Headers.Add("access-control-expose-headers", "Pagination");
			}
		}

	}
}
