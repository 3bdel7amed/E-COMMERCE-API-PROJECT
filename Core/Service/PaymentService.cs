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
					PaymentMethod = "card"
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
	}
}
