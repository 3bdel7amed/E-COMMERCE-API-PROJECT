namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class BasketController(IServiceManager serviceManager) : ControllerBase
	{
		[HttpGet("{BasketId}")] 
		public async Task<ActionResult<CustomerBasketResultDto>> GetBasket(string BasketId)
		{
			var Basket = await serviceManager.BasketService().GetBasketAsync(BasketId);
			return Ok(Basket);
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasketResultDto>> UpdateBasket(CustomerBasketResultDto _Basket)
		{
			var Basket = await serviceManager.BasketService().UpdateBasketAsync(_Basket);
			return Ok(Basket);
		}
		[HttpDelete("{BasketId}")] 
		public async Task<ActionResult> DeleteBasket(string BasketId)
		{
			var Basket = await serviceManager.BasketService().DeleteBasketAsync(BasketId);
			return NoContent();
		}
	}
}
