using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Enums;

namespace ApplicationCore.Helpers
{
	public class PagedList<T> : List<T>
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }

		private PagedList(List<T> items, int count, int pageNumber, int pageSize)
		{
			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			this.AddRange(items);
		}

		public static PagedList<T> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, 
				string sortProperty, string orderBy)
		{
			var count = source.Count();
			if (!string.IsNullOrEmpty(sortProperty))
			{
				if (orderBy?.ToLower() == nameof(OrderByEnum.MostExpensive).ToLower())
					source = source.OrderByDescending(sortProperty);
				else
					source = source.OrderBy(sortProperty);
			}

			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
	}
}
