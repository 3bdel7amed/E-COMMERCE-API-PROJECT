
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
