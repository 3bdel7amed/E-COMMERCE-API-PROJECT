using System.Security.Claims;

namespace Presentation
{
	[Authorize]
	public class OrderController(IServiceManager serviceManager) : ApiController
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResultDto>> Order(Guid id) =>
			Ok(await serviceManager.OrderService().GetOrderByIdAsync(id));
		[HttpPost]
		public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequestDto request)
		{
			string email = User.FindFirstValue(ClaimTypes.Email);
			return Ok(await serviceManager.OrderService().CreateOrderAsync(request, email));
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderResultDto>>> Orders()
		{
			string email = User.FindFirstValue(ClaimTypes.Email);
			return Ok(await serviceManager.OrderService().GetOrdersByEmailAsync(email));
		}

		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<IEnumerable<DeliveryMethodsResultDto>>> DeliveryMethods() =>
			Ok(await serviceManager.OrderService().GetDeliveryMethodsAsync());
	}
}
