using Shared;

namespace Service.Abstraction
{
	public interface IProductService
	{
		// Get All Brands
		public Task<IEnumerable<BrandResultDto>> GetBrandsAsync();
		// Get All Types
		public Task<IEnumerable<TypeResultDto>> GetTypesAsync();
		// Get All Products
		public Task<IEnumerable<ProductResultDto>> GetProductsAsync();
		// Get Product By Id
		public Task<ProductResultDto> GetProductAsync(int Id);
	}
}
