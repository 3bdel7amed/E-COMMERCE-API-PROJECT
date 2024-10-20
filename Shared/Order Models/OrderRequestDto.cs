using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order_Models
{
	public class OrderRequestDto
	{
        public int BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressResultDto ShippingAddress { get; set; }
    }
}
