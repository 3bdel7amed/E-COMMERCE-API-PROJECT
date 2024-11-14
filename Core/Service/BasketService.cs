using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.BasketModels;

namespace Service
{
    public class BasketService : IBasketService
	{
		readonly IBasketRepo basketRepo;
		readonly IMapper mapper;

		public BasketService(IBasketRepo basketRepo, IMapper mapper)
		{
			this.basketRepo = basketRepo;
			this.mapper = mapper;
		}
		public async Task<bool> DeleteBasketAsync(string BasketId)
		{
			return await basketRepo.DeleteBasketAsync(BasketId);
		}
		public async Task<CustomerBasketResultDto> GetBasketAsync(string BasketId)
		{
			var Basket = await basketRepo.GetBasketAsync(BasketId);
			return Basket is null ? new CustomerBasketResultDto(BasketId)
				: mapper.Map<CustomerBasketResultDto>(Basket);
		}
		public async Task<CustomerBasketResultDto> UpdateBasketAsync(CustomerBasketResultDto Basket)
		{
			var UpdatedBasket = await basketRepo.UpdateBasketAsync(mapper.Map<CustomerBasket>(Basket));
			return UpdatedBasket is not null ? mapper.Map<CustomerBasketResultDto>(UpdatedBasket)
				: throw new Exception("Can't Update Basket Now !");
		}
	}
}
