using Microsoft.Extensions.Configuration;
namespace Service.MappingProfiles
{
	// Inject Config
	internal class PictureResolver<TSource, TDestination>(IConfiguration configuration) : IValueResolver<TSource, TDestination, string>
	{
		// Must Have Property "PictureUrl" To Map It
		public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
			=> !string.IsNullOrWhiteSpace(typeof(TSource).GetProperty("PictureUrl")?.GetValue(source).ToString()) ?
			$"{configuration["BaseUrl"]}/{typeof(TSource).GetProperty("PictureUrl")?.GetValue(source)}" :
			string.Empty;

	}
}
