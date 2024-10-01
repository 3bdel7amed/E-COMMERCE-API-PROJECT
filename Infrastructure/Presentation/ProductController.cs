

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController(IServiceManager ServiceManager) : ControllerBase
	{
		[HttpGet] // Index
		public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetProducts()
		{
			var Products = ServiceManager.ProductService().GetProductsAsync();
			return Ok(Products);
		}
		[HttpGet("Brands")]
		public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetBrands()
		{
			var brands = await ServiceManager.ProductService().GetBrandsAsync();
			return Ok(brands);
		}
		[HttpGet("Types")]
		public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetTypes()
		{
			var Types = ServiceManager.ProductService().GetTypesAsync();
			return Ok(Types);
		}
		[HttpGet("{Id}")] 
		public async Task<ActionResult<ProductResultDto>> GetProduct(int Id)
		{
			var Product = ServiceManager.ProductService().GetProductAsync(Id);
			return Ok(Product);
		}
	}
}
