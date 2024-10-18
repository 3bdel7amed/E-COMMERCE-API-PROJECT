
namespace Service.Abstraction
{
	public interface IServiceManager
	{
		public IProductService ProductService();
		public IBasketService BasketService();
		public IAuthenticationService AuthenticationService();
	
	}
}
