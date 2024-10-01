
namespace Service
{
	public class ServiceManager : IServiceManager
	{
		readonly Lazy<IProductService> _productService;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
			=> _productService = new Lazy<IProductService>(
				() => new ProductService(unitOfWork, mapper));
		public IProductService ProductService()=>_productService.Value;
	}
}
