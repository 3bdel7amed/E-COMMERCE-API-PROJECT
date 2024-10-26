﻿
namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IProductService> _productService;
		readonly Lazy<IBasketService> _basketService;
		readonly Lazy<IAuthenticationService> _authenticationService;
		readonly Lazy<IOrderService> _orderService;
		public ServiceManager(
			IUnitOfWork unitOfWork,
			IBasketRepo basketRepo,
			IMapper mapper,
			UserManager<User> userManager,
			IOptions<JwtOptions> jwtOptions)
		{
			_productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
			_basketService = new Lazy<IBasketService>(() => new BasketService(basketRepo, mapper));
			_authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, jwtOptions, mapper));
			_orderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, basketRepo, mapper));
		}
		public IProductService ProductService() => _productService.Value;
		public IBasketService BasketService() => _basketService.Value;
		public IAuthenticationService AuthenticationService() => _authenticationService.Value;
		public IOrderService OrderService() => _orderService.Value;

	}
}
