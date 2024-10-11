
using Service.Specifications;

namespace Service
{
	class ProductService(IUnitOfWork UnitOfWork, IMapper Mapper) : IProductService
	{
		public async Task<IEnumerable<BrandResultDto>> GetBrandsAsync()
		{
			// retrieve brands -> UnitOfWork
			var Brands = await UnitOfWork.GetRepo<ProductBrand, int>().GetAllAsync();
			// map to brandResultDto -> Mapper
			var BrandsDto = Mapper.Map<IEnumerable<BrandResultDto>>(Brands);
			// return
			return BrandsDto;
		}

		public async Task<IEnumerable<TypeResultDto>> GetTypesAsync()
		{
			var Types = await UnitOfWork.GetRepo<ProductType, int>().GetAllAsync();
			var TypesDto = Mapper.Map<IEnumerable<TypeResultDto>>(Types);
			return TypesDto;
		}

		public async Task<PaginatedResultDto<ProductResultDto>> GetProductsAsync(string? sort, int? brandId, int? typeId, int pageSize , int pageIndex , string? search)
		{
			var Products = await UnitOfWork.GetRepo<Product, int>().GetAllAsync(new ProductSpecifications(sort, brandId, typeId, pageSize, pageIndex,search));
			var ProductsDto = Mapper.Map<IEnumerable<ProductResultDto>>(Products);
			var totalProducts = await UnitOfWork.GetRepo<Product, int>().GetAllAsync(new PaginatedResultSpecifications(sort, brandId, typeId, pageSize, pageIndex,search));
			var PaginatedResult = new PaginatedResultDto<ProductResultDto>(
				ProductsDto.Count(),
				pageIndex,
				totalProducts.Count(), 
				ProductsDto);
			return PaginatedResult;
		}

		public async Task<ProductResultDto> GetProductAsync(int Id)
		{
			var Product = await UnitOfWork.GetRepo<Product, int>().GetAsync(new ProductSpecifications(Id));
			var productsDto = Mapper.Map<ProductResultDto>(Product);
			return productsDto;
		}

		
	}
}
