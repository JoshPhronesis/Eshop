using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
	public class UserParams
	{
		private const int maxPageSize = 12;
		public int PageNumber { get; set; } = 1;
		private int pageSize = 10;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = (value > maxPageSize ? maxPageSize : value); }
		}
		public decimal MinPrice { get; set; } = 1;
		public decimal MaxPrice { get; set; } = 100000;
		public string OrderBy { get; set; }
		public string SearchTerm { get; set; }
	}
}
