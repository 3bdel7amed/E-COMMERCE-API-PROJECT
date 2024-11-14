using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderModule
{
	public class Order : BaseEntity<Guid>
	{
		public Order() { }
		public Order(string email,
			Address shippingAddress,
			ICollection<OrderItem> items,
			decimal subTotal,
			DeliveryMethods deliveryMethod
,
			string paymentIntentId)
		{
			Email = email;
			ShippingAddress = shippingAddress;
			Items = items;
			SubTotal = subTotal;
			DeliveryMethod = deliveryMethod;
			PaymentIntentId = paymentIntentId;
		}

		public string Email { get; set; }
		public Address ShippingAddress { get; set; }
		public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
		public decimal SubTotal { get; set; }
		public DeliveryMethods DeliveryMethod { get; set; }
		public int? DeliveryMethodId { get; set; }
		public string PaymentIntentId { get; set; } 
		public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
		public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
	}
}
