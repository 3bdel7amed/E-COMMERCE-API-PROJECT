using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class BasketItem
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public string Category { get; set; }
		public decimal Price { get; set; }
		public string BrandName { get; set; }
		public string TypeName { get; set; }
	}
}
