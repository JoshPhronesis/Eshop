﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IRepository<T, Tid> where T: BaseEntity<Tid>
	{
		Task<T> GetByIdAsync(Tid id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task DeleteAsync(T entity);
		Task UpdateAsync(T entity);
		Task<int> CountAsync(T entity);
	}
}
