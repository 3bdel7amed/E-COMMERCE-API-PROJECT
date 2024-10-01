
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

		public async Task<IEnumerable<ProductResultDto>> GetProductsAsync()
		{
			var Products = await UnitOfWork.GetRepo<Product, int>().GetAllAsync();
			var productsDto = Mapper.Map<IEnumerable<ProductResultDto>>(Products);
			return productsDto;
		}

		public async Task<ProductResultDto> GetProductAsync(int Id)
		{
			var Product = await UnitOfWork.GetRepo<Product, int>().GetAsync(Id);
			var productsDto = Mapper.Map<ProductResultDto>(Product);
			return productsDto;
		}
	}
}
