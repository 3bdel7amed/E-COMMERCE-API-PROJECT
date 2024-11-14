namespace Service
{
	internal class PaymentService(IBasketRepo basketRepo,
		IUnitOfWork unitOfWork,
		IConfiguration configuration,
		IMapper mapper) : IPaymentService
	{
		public async Task<CustomerBasketResultDto> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			// Connect Stripe
			StripeConfiguration.ApiKey = configuration.GetRequiredSection("Stripe")["SecretKey"];

			var basket = await basketRepo.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

			// Calculate Amount
			foreach (var item in basket.Items)
				item.Price = (await unitOfWork.GetRepo<Product, int>().GetAsync(item.Id) ??
					throw new ProductNotFoundException(item.Id)).Price;
			var subTotal = basket.Items.Sum(item => item.Price * item.Quantity);

			if (!basket.DeliveryMethodId.HasValue) throw new NotFoundException("Not Found Delivery Method!");
			var deliveryMethodCost = (await unitOfWork.GetRepo<DeliveryMethods, int>().GetAsync(basket.DeliveryMethodId.Value) ??
					throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value)).Cost;

			long amount = (long)(subTotal + deliveryMethodCost) * 100;

			// Create Payment Intent
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
			{
				// Create Payment Intent Id
				var options = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = "USD",
					PaymentMethodTypes = new List<string>() { "card" }
				};
				paymentIntent = await service.CreateAsync(options);

				basket.ClintSecret = paymentIntent.ClientSecret;
				basket.PaymentIntentId = paymentIntent.Id;
			}
			else
			{
				// Update Payment Intent Id
				var options = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};
				paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			// Update Basket
			await basketRepo.UpdateBasketAsync(basket);

			// Return
			return mapper.Map<CustomerBasketResultDto>(basket);
		}

		public async Task UpdatePaymentStatusAsync(string request, string header)
		{
			var endPoint = configuration.GetRequiredSection("Stripe")["EndPointSecret"];
			var stripeEvent = EventUtility.ConstructEvent(request, header, endPoint);
			var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

			switch (stripeEvent.Type)
			{
				case EventTypes.PaymentIntentPaymentFailed:
					await UpdatePaymentIntentFailed(paymentIntent.Id);
					break;
				case EventTypes.PaymentIntentSucceeded:
					await UpdatePaymentIntentSucceeded(paymentIntent.Id);

					break;
			}


			throw new NotImplementedException();
		}

		private async Task UpdatePaymentIntentFailed(string id)
		{
			var order = await unitOfWork.GetRepo<Order, Guid>().GetAsync(new OrderWithPaymentIntent(id))
				?? throw new NotFoundException("Order Not Found!");
			order.Status = PaymentStatus.Failed;
			unitOfWork.GetRepo<Order, Guid>().Update(order);
			await unitOfWork.SaveChangesAsync();
		}

		private async Task UpdatePaymentIntentSucceeded(string id)
		{
			var order = await unitOfWork.GetRepo<Order, Guid>().GetAsync(new OrderWithPaymentIntent(id))
							?? throw new NotFoundException("Order Not Found!");
			order.Status = PaymentStatus.Received;
			unitOfWork.GetRepo<Order, Guid>().Update(order);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
