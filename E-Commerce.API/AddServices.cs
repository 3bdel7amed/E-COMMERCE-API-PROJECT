namespace E_Commerce.API
{
	public static class AddServices
	{
		public static IServiceCollection ContainerServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Controller Service
			services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
			// DbInitializer Service
			services.AddScoped<IDbInitializer, DbInitializer>();
			// ServiceManager Service
			services.AddScoped<IServiceManager, ServiceManager>();
			// UnitOfWork Service
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			// AutoMapper Service
			services.AddAutoMapper(typeof(AssemblyReference).Assembly);
			// SQL Service
			services.AddDbContext<StoreContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultSQLConnection")));
			// Error Handling
			services.Configure<ApiBehaviorOptions>(Options => Options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse);
			// Redis Service
			services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisDb")!));
			// Basket Repo Service
			services.AddScoped<IBasketRepo, BasketRepo>();
			// Basket Service
			services.AddScoped<IBasketService, BasketService>();
			
			return services;
		}
	}
}
