using Domain.Entities;
using Shared.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IBasketService
	{
		public Task<CustomerBasketResultDto> GetBasketAsync(string BasketId);
		public Task<bool> DeleteBasketAsync(string BasketId); 
		public Task<CustomerBasketResultDto> UpdateBasketAsync(CustomerBasketResultDto Basket);
	}
}
