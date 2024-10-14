using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Specifications
{
	public abstract class Specifications<T>
	{
		// Ctor To Init Criteria 
		public Specifications(Expression<Func<T, bool>>? criteria) => Criteria = criteria;
		// Props
		public Expression<Func<T, bool>>? Criteria { get; }
		public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
		public Expression<Func<T, object>> OrderBy { get; private set; }
		public Expression<Func<T, object>> OrderByDesc { get; private set; }
		public int Skip { get; private set; }
		public int Take { get; private set; }
        public bool IsPaginated { get; private set; }
        // Add Include To Include List
        protected void AddInclude(Expression<Func<T, object>> Ex) => IncludeExpressions.Add(Ex);
		// Set Order By
		protected void SetOrderBy(Expression<Func<T, object>> Ex) => OrderBy = Ex;
		protected void SetOrderByDesc(Expression<Func<T, object>> Ex) => OrderByDesc = Ex;
		// Set Pagination
		protected void ApplyPagination(int PageSize,int PageIndex)
		{
			IsPaginated= true;
			Take = PageSize;
			Skip = (PageIndex - 1) * PageSize;
		}
	}
}
