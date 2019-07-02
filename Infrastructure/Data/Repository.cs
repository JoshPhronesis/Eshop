﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class Repository<T, Tid> : IRepository<T, Tid> where T : BaseEntity<Tid>
	{
		private readonly DataContext context;
		public Repository(DataContext context)
		{
			this.context = context;
		}

		public async Task<T> AddAsync(T entity)
		{
			context.Set<T>().Add(entity);
			await context.SaveChangesAsync();
			return entity;
		}

		public async Task<int> CountAsync(T entity)
		{
			return await context.Set<T>().CountAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			context.Set<T>().Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await context.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(Tid id)
		{
			return await context.Set<T>().FindAsync(id);
		}

		public async Task UpdateAsync(T entity)
		{
			context.Set<T>().Update(entity);
			await context.SaveChangesAsync();
		}
	}
}