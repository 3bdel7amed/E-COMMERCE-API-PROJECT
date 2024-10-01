using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
	// Inject Config
	internal class PictureResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
	{
		public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrWhiteSpace(source.PictureUrl)) return "";
			return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
			// baseUrl/pictureUrl 
		}
	}
}
