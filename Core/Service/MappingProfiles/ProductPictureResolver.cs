using Microsoft.Extensions.Configuration;
using Shared.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
	internal class ProductPictureResolver(IConfiguration configuration) : IValueResolver<Product,ProductResultDto,string>
	{
		public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrWhiteSpace(source.PictureUrl)) return "";
			return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
			// baseUrl/pictureUrl 
		}
	}
}
