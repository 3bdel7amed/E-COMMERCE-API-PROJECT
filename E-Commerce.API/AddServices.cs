
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.UserModels;
using System.Text;
using Service.Abstraction;
using Service;
using IAuthenticationService = Microsoft.AspNetCore.Authentication.IAuthenticationService;
using AuthenticationService = Microsoft.AspNetCore.Authentication.AuthenticationService;

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
			// Identity Db Service
			services.AddDbContext<StoreIdentityContext>(o => o.UseSqlServer(configuration.GetConnectionString("IdentitySQLConnection")));
			// User & Role Manager Service
			services.AddIdentity<User, IdentityRole>(o =>
			{
				o.Password.RequireLowercase=false;
				o.Password.RequireUppercase=false;
				o.Password.RequireNonAlphanumeric=false;
				o.Password.RequireDigit=false;
				//
				o.Password.RequiredLength = 8;
				o.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<StoreIdentityContext>();
			// Authentication Service
			services.AddScoped<IAuthenticationService,AuthenticationService>();
			// Jwt Configure
			services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
			// Jwt Add Configurations
			services.JwtConfigurations(configuration);





			return services;
		}

		private static IServiceCollection JwtConfigurations(this IServiceCollection services,IConfiguration configuration)
		{
			var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidAudience = jwtOptions.Audience,
					ValidIssuer = jwtOptions.Issuer,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
				};
			});
			services.AddAuthorization(); // Policy
			return services;
		}
	}
}
