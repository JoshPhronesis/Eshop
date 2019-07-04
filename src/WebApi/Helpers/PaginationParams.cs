using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
	public class PaginationParams
	{
		private const int maxPageSize = 12;
		public int PageNumber { get; set; } = 1;
		private int pageSize = 10;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = (value > maxPageSize ? maxPageSize : value); }
		}
	}
}
