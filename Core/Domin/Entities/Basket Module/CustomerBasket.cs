using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class CustomerBasket
	{
		public CustomerBasket(string basketId) => BasketId = basketId;
		public string BasketId { get; set; }
		public IEnumerable<BasketItem> Items { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? ClintSecret { get; set; }
		public int? DeliveryMethodId { get; set; }
		//public decimal? ShippingPrice { get; set; }
	}
}
