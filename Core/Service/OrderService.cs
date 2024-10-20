using Domain.Entities.OrderModule;
using OrderAddress = Domain.Entities.OrderModule.Address;
using Shared.Order_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Service.Specifications;

namespace Service
{
	internal class OrderService(IUnitOfWork unitOfWork, IBasketRepo basketRepo, IMapper mapper) : IOrderService
	{
		public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto request, string email)
		{
			var address = mapper.Map<OrderAddress>(request.ShippingAddress);

			var basketItems = (await basketRepo.GetBasketAsync(request.BasketId)
				?? throw new BasketNotFoundException(request.BasketId)).Items;

			var orderItems = new List<OrderItem>();


			foreach (var Item in basketItems)
			{
				var product = await unitOfWork.GetRepo<Product, int>().GetAsync(Item.Id)
					?? throw new ProductNotFoundException(Item.Id);

				orderItems.Add(new OrderItem(new ProductItem(
					product.Id, product.Name, product.PictureUrl),
						Item.Quantity, product.Price));
			}

			var deliveryMethod = await unitOfWork.GetRepo<DeliveryMethods, int>().GetAsync(request.DeliveryMethodId)
				?? throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

			var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

			var order = new Order(
				email: email,
				shippingAddress: address,
				items: orderItems,
				deliveryMethod: deliveryMethod,
				subTotal: subTotal);

			await unitOfWork.GetRepo<Order, Guid>().AddAsync(order);
			await unitOfWork.SaveChangesAsync();

			return mapper.Map<OrderResultDto>(order);
		}

		public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
		{
			var order = await unitOfWork.GetRepo<Order,Guid>()
				.GetAsync(new OrderSpecifications(id));
			return order is null ? throw new OrderNotFoundException(id)
				:mapper.Map<OrderResultDto>(order);
		}

		public async Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email)
		{
			var orders = await unitOfWork.GetRepo<Order, Guid>()
				.GetAllAsync(new OrderSpecifications(email));
			return orders is null ?new List<OrderResultDto>()
				: mapper.Map<IEnumerable<OrderResultDto>>(orders);
		}

		public async Task<IEnumerable<DeliveryMethodsResultDto>> GetDeliveryMethodsAsync()
		{
			var deliveryMethods = await unitOfWork.GetRepo<DeliveryMethods, int>()
				.GetAllAsync();
            return deliveryMethods is null ? new List<DeliveryMethodsResultDto>()
				: mapper.Map<IEnumerable<DeliveryMethodsResultDto>>(deliveryMethods);
		}

	}
}
