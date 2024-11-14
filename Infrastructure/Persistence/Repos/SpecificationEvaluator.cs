using Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
	public static class SpecificationEvaluator
	{
		public static IQueryable<T> GetQuery<T>(IQueryable<T> InQuery, Specifications<T> specifications) where T : class
		{
			var query = InQuery;
			if (specifications.Criteria is not null) query = query.Where(specifications.Criteria);
			foreach (var item in specifications.IncludeExpressions) query = query.Include(item);
			if (specifications.IsPaginated)
			{
				query = query.Skip(specifications.Skip).Take(specifications.Take);
			}
			return query;
		}
	}
}
