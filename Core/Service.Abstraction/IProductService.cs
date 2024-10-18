using Shared.ProductModels;

namespace Service.Abstraction
{
    public interface IProductService
	{
		// Get All Brands
		public Task<IEnumerable<BrandResultDto>> GetBrandsAsync();
		// Get All Types
		public Task<IEnumerable<TypeResultDto>> GetTypesAsync();
		// Get All Products
		public Task<PaginatedResultDto<ProductResultDto>> GetProductsAsync
			(string? sort, int? brandId, int? typeId, int pageSize, int pageIndex ,string? search);
		// Get Product By Id
		public Task<ProductResultDto> GetProductAsync(int Id);
	}
}
