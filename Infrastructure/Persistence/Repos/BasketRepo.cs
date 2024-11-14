using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
	public class BasketRepo : IBasketRepo
	{
		readonly IDatabase database;
		public BasketRepo(IConnectionMultiplexer connection) => database = connection.GetDatabase();
		public async Task<bool> DeleteBasketAsync(string BasketId) => await database.KeyDeleteAsync(BasketId);

		public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
		{
			var Basket =  await database.StringGetAsync(BasketId);
			return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket!);
		}
		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
		{
			var jsonBasket = JsonSerializer.Serialize(Basket);
			bool CreateOrUpdateBasket = await database.StringSetAsync(Basket.BasketId, jsonBasket,TimeSpan.FromDays(10));
			return CreateOrUpdateBasket ? await GetBasketAsync(Basket.BasketId) : null;
		}
	}
}
