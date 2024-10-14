using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class CustomerBasket
	{
		public string BasketId { get; set; }
		public IEnumerable<BasketItem> Items { get; set; }
		public CustomerBasket(string basketId) => BasketId = basketId;
	}
}
