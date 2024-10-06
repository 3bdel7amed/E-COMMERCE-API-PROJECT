using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Specifications
{
	public class Specifications<T>
	{
        public Expression<Func<T,bool>>? Criteria { get; }
		public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        public Specifications(Expression<Func<T, bool>>? criteria)
		{
			Criteria = criteria;
		}
		public void AddInclude(Expression<Func<T, object>> Ex) => IncludeExpressions.Add(Ex);
	}
}
