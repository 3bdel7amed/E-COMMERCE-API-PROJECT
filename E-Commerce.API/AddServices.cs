using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Service;

namespace E_Commerce.API
{
	public static class AddServices
	{
		public static IServiceCollection ContainerServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
			services.AddScoped<IDbInitializer, DbInitializer>();
			services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(typeof(AssemblyReference).Assembly);
			services.AddDbContext<StoreContext>
				(o => o.UseSqlServer(configuration.GetConnectionString("DefaultSQLConnection")));
			services.Configure<ApiBehaviorOptions>(Options =>
			{
				Options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
			});

			return services;
		}
	}
}
