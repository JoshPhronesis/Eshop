using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data
{
	public class Repository<T, Tid> : IRepository<T, Tid> where T : BaseEntity<Tid>
	{
		public async Task<T> AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public async Task<int> CountAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetByIdAsync(Tid id)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
