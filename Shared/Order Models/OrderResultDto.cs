using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order_Models
{
	public class OrderResultDto
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public AddressResultDto ShippingAddress { get; set; }
		public ICollection<OrderItemResultDto> Items { get; set; } = new List<OrderItemResultDto>();
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }
		public string DeliveryMethod { get; set; }
		public string PaymentIntentId { get; set; } = string.Empty;
		public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
		public string Status { get; set; } 
	}
}
