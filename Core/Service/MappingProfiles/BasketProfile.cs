﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.BasketModels;

namespace Service.MappingProfiles
{
    public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			CreateMap<CustomerBasket,CustomerBasketResultDto>().ReverseMap();
			CreateMap<BasketItem,BasketItemDto>().ReverseMap();
		}
	}
}
