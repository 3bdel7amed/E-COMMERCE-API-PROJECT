using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.IRepos
{
	public interface IBasketRepo
	{
		public Task<CustomerBasket?> GetBasketAsync(string BasketId);
		public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);
		public Task<bool> DeleteBasketAsync(string BasketId);

	}
}
