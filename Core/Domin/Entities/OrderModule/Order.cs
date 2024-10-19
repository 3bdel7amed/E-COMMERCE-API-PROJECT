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
			DeliveryMethod deliveryMethod
			)
		{
			Email = email;
			ShippingAddress = shippingAddress;
			Items = items;
			SubTotal = subTotal;
			DeliveryMethod = deliveryMethod;
		}

		public string Email { get; set; }
		public Address ShippingAddress { get; set; }
		public ICollection<OrderItem> Items { get; set; }
		public decimal SubTotal { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public int? DeliveryMethodId { get; set; }
		public string PaymentIntentId { get; set; } = string.Empty;
		public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
		public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
	}
}
