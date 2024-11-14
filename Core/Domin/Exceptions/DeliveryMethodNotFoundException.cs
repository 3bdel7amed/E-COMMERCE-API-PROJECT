﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class DeliveryMethodNotFoundException(int id)
		: NotFoundException($"Delivery Method With {id} Is Not Found!")
	{
	}
}
