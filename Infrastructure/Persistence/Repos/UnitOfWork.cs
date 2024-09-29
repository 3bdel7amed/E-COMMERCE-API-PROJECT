using Persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
	public class UnitOfWork : IUnitOfWork
	{
		readonly StoreContext storeContext;
		//readonly Dictionary<string, object> storedRepos;
		readonly ConcurrentDictionary<string, object> storedRepos;
		public UnitOfWork(StoreContext _storeContext, ConcurrentDictionary<string, object> _storedRepos/*,Dictionary<string, object> _storedRepos*/)
		{
			storeContext = _storeContext;
			storedRepos = _storedRepos;
			//storedRepos = _storedRepos;
		}
		public async Task<int> SaveChangesAsync() => await storeContext.SaveChangesAsync();

		public IGenericRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
			=> (IGenericRepo<TEntity, TKey>) // Casting
			storedRepos.GetOrAdd(typeof(TEntity).Name,
				_ => new GenericRepo<TEntity, TKey>(storeContext));
		#region Dictionary
		//{
		//string type = typeof(TEntity).Name;

		//if (storedRepos.ContainsKey(type))  return (IGenericRepo<TEntity, TKey>)storedRepos[type];

		//var repo = new GenericRepo<TEntity, TKey>(storeContext);

		//storedRepos[type] = repo;

		//return repo; 
		//}
		#endregion

	}
}
