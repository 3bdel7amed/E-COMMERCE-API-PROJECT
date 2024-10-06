namespace Service.MappingProfiles
{
	internal class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductResultDto>().
				ForMember(b => b.BrandName,
				o => o.MapFrom(s => s.ProductBrand.Name)).
				ForMember(t => t.TypeName,
				o => o.MapFrom(s => s.ProductType.Name)).
				ForMember(p=>p.PictureUrl,
				o=>o.MapFrom<PictureResolver>());
			// class inherit from IValueResolver

			CreateMap<ProductBrand, BrandResultDto>();
			
			CreateMap<ProductType, TypeResultDto>();
		}
	}
}
