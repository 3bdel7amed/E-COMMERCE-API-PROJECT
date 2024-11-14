//using OrderAddress =  Domain.Entities.OrderModule.Address;
using Shared.Order_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModule;

namespace Service.MappingProfiles
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<DeliveryMethods,DeliveryMethodsResultDto>()
				.ReverseMap();
			
			CreateMap<OrderAddress,AddressResultDto>()
				.ReverseMap();

			CreateMap<OrderItem, OrderItemResultDto>()
				.ForMember(d => d.Id, o => o
				.MapFrom(s => s.Product.Id))
				.ForMember(d => d.Name, o => o
				.MapFrom(s => s.Product.Name))
				.ForMember(d => d.PictureUrl, o => o
				.MapFrom<PictureResolver<OrderItem,OrderItemResultDto>>())
				.ReverseMap();

			CreateMap<Order, OrderResultDto>()
				.ForMember(d => d.Total, o => o
				.MapFrom(s => s.SubTotal + s.DeliveryMethod.Cost))
				.ForMember(d => d.DeliveryMethod, o => o
				.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(d => d.Status, o => o
				.MapFrom(s => s.Status.ToString()))
				.ReverseMap();

		}
	}
}
