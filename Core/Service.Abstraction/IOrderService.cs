using Shared.Order_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
	public interface IOrderService
	{
		// Get Order By Id
		public Task<OrderResultDto> GetOrderByIdAsync(Guid id);
		// Get Orders By Email
		public Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email);
		// Get Delivery Methods
		public Task<IEnumerable<DeliveryMethodsResultDto>> GetDeliveryMethodsAsync();
		// Create Order
		public Task<OrderResultDto> CreateOrderAsync(OrderRequestDto request, string email);

	}
}
