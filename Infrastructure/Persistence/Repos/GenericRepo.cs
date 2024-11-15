﻿using Domain.Contracts.Specifications;
using Persistence.Data;
using Service.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
	public class GenericRepo<TEntity, TKey> : IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		readonly StoreContext storeContext;
		public GenericRepo(StoreContext storeContext) => this.storeContext = storeContext;
		public async Task<IEnumerable<TEntity?>> GetAllAsync(bool tracking = false)
			=> tracking ? await storeContext.Set<TEntity>().ToListAsync() :
		 		await storeContext.Set<TEntity>().AsNoTracking().ToListAsync();
		public async Task<TEntity?> GetAsync(TKey Id) => await storeContext.Set<TEntity>().FindAsync(Id);

		public async Task<TEntity?> GetAsync(Specifications<TEntity> specifications)
			=> await SpecificationEvaluator.GetQuery(storeContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
		public async Task<IEnumerable<TEntity?>> GetAllAsync(Specifications<TEntity> specifications)
			=> await SpecificationEvaluator.GetQuery(storeContext.Set<TEntity>(), specifications).ToListAsync();

		public async Task AddAsync(TEntity entity) => await storeContext.Set<TEntity>().AddAsync(entity);
		public void Delete(TEntity entity) => storeContext.Set<TEntity>().Remove(entity);
		public void Update(TEntity entity) => storeContext.Set<TEntity>().Update(entity);
		IQueryable<TEntity> ApplySpecs(Specifications<TEntity> specifications)
			=> SpecificationEvaluator.GetQuery(storeContext.Set<TEntity>(), specifications);
	}
}
