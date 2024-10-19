using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderModule
{
	public class Order : BaseEntity<Guid>
	{
		public string Email { get; set; }
		public Address ShippingAddress { get; set; }
		public ICollection<OrderItem> Items { get; set; }
		public PaymentStatus Status { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; }
		public string PaymentIntentId { get; set; } = string.Empty;
		public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
	}
}
