using Domain.Entities.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.Order_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
	internal class OrderPictureResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemResultDto, string>
	{
		public string Resolve(OrderItem source, OrderItemResultDto destination, string destMember, ResolutionContext context)
			=> string.IsNullOrWhiteSpace(source.Product.PictureUrl) ?
				string.Empty :
				$"{configuration["BaseUrl"]}/{source.Product.PictureUrl}";

		//if (string.IsNullOrWhiteSpace(source.Product.PictureUrl))	return string.Empty;
		//return $"{configuration["BaseUrl"]}/{source.Product.PictureUrl}";

	}
}
