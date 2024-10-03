using Domain.Contracts.Specifications;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.IRepos
{
	public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public Task<TEntity?> GetAsync(TKey Id);
		public Task<IEnumerable<TEntity?>> GetAllAsync(bool tracking = false);
		public Task<TEntity?> GetAsync(Specifications<TEntity> specifications);
		public Task<IEnumerable<TEntity?>> GetAllAsync(Specifications<TEntity> specifications);
		public Task AddAsync(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);
	}
}
