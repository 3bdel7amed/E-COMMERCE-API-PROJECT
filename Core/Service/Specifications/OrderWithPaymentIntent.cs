using Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
	internal class OrderWithPaymentIntent : Specifications<Order>
	{
		public OrderWithPaymentIntent(string PaymentIntentId) : base(o => o.PaymentIntentId == PaymentIntentId)
		{
		}
	}
}
