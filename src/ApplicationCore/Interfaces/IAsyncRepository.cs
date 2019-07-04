using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IAsyncRepository<T, Tid> where T: BaseEntity<Tid>
	{
		Task<T> GetByIdAsync(Tid id);
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task DeleteAsync(T entity);
		Task UpdateAsync(T entity);
		Task<int> CountAsync(T entity);
		IQueryable<T> GetAllAsQueryable();
	}
}
