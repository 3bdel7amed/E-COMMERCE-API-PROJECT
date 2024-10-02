

using Service;
using Service.Abstraction;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddScoped<IServiceManager, ServiceManager>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
			builder.Services.AddDbContext<StoreContext>
				(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			await DataSeeding(app);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

		}
		// Data Seeding Method
		static async Task DataSeeding(WebApplication app)
		{
			// Create Scope 
			using var scope = app.Services.CreateScope();
			// Inject
			var initDb = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
			// Call Initializer
			await initDb.InitializerAsync();
		}
	}
}
