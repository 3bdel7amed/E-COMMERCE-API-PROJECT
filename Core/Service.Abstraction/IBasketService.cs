
namespace Service.Abstraction
{
    public interface IBasketService
	{
		public Task<CustomerBasketResultDto> GetBasketAsync(string BasketId);
		public Task<bool> DeleteBasketAsync(string BasketId); 
		public Task<CustomerBasketResultDto> UpdateBasketAsync(CustomerBasketResultDto Basket);
	}
}
