using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
	public interface IPaymentService
	{
		public Task<CustomerBasketResultDto> CreateOrUpdatePaymentIntentAsync(string basket);
	}
}
