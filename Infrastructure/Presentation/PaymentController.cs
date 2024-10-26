using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	public class PaymentController(IServiceManager serviceManager) : ApiController
	{
		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasketResultDto>> CreateOrUpdatePaymentIntentId(string basketId) =>
			Ok(await serviceManager.PaymentService().CreateOrUpdatePaymentIntentAsync(basketId));
	}
}
