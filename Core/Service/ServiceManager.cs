
namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IProductService> _productService;
		readonly Lazy<IBasketService> _basketService;
		readonly Lazy<IAuthenticationService> _authenticationService;
		readonly Lazy<IOrderService> _orderService;
		readonly Lazy<IPaymentService> _paymentService;
		public ServiceManager(
			IUnitOfWork unitOfWork,
			IBasketRepo basketRepo,
			UserManager<User> userManager,
			IOptions<JwtOptions> jwtOptions,
			IConfiguration configuration,
			IMapper mapper)
		{
			_productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
			_basketService = new Lazy<IBasketService>(() => new BasketService(basketRepo, mapper));
			_authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, jwtOptions, mapper));
			_orderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, basketRepo, mapper));
			_paymentService = new Lazy<IPaymentService>(() => new PaymentService(basketRepo, unitOfWork, configuration, mapper));
		}
		public IProductService ProductService() => _productService.Value;
		public IBasketService BasketService() => _basketService.Value;
		public IAuthenticationService AuthenticationService() => _authenticationService.Value;
		public IOrderService OrderService() => _orderService.Value;
		public IPaymentService PaymentService() => _paymentService.Value;
	}
}
