
namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IProductService> _productService;
		readonly Lazy<IBasketService> _basketService;
		public ServiceManager(IUnitOfWork unitOfWork, IBasketRepo basketRepo, IMapper mapper)
		{
			_productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
			_basketService = new Lazy<IBasketService>(()=> new BasketService(basketRepo, mapper));
		}
		public IProductService ProductService() => _productService.Value;
		public IBasketService BasketService() => _basketService.Value;

	}
}
