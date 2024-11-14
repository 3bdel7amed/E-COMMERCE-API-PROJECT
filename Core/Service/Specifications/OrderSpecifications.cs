using Domain.Contracts.Specifications;
using Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
	public class OrderSpecifications : Specifications<Order>
	{
		public OrderSpecifications(Guid id) : base(o => o.Id == id)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.Items);
		}
		public OrderSpecifications(string email) : base(o => o.Email == email)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.Items);
			SetOrderBy(o => o.Date);
		}
	}
}
