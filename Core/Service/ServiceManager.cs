
using Microsoft.AspNetCore.Identity;

namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IProductService> _productService;
		readonly Lazy<IBasketService> _basketService;
		readonly Lazy<IAuthenticationService> _authenticationService;
		public ServiceManager(IUnitOfWork unitOfWork, IBasketRepo basketRepo, IMapper mapper,UserManager<User> userManager)
		{
			_productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
			_basketService = new Lazy<IBasketService>(()=> new BasketService(basketRepo, mapper));
			_authenticationService = new Lazy<IAuthenticationService>(()=> new AuthenticationService(userManager, mapper));
		
		}
		public IProductService ProductService() => _productService.Value;
		public IBasketService BasketService() => _basketService.Value;
		public IAuthenticationService AuthenticationService() => _authenticationService.Value;

	}
}
