using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using Persistence.IdentityData;
using System;
using System.Globalization;

namespace Persistence.Repos
{
	public class DbInitializer : IDbInitializer
	{
		readonly StoreContext storeContext;
		readonly StoreIdentityContext storeIdentityContext;
		readonly UserManager<User> userManager;
		readonly RoleManager<IdentityRole> roleManager;

		public DbInitializer(StoreContext _storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, StoreIdentityContext storeIdentityContext)
		{
			storeContext = _storeContext;
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.storeIdentityContext = storeIdentityContext;
		}

		public async Task InitializerAsync()
		{
			try
			{
				// Check Pending Migrations

				if (storeContext.Database.GetPendingMigrations().Any())
					await storeContext.Database.MigrateAsync();

				// Data Seeding

				if (!storeContext.ProductBrands.Any())
				{
					// Read As A String
					var brandsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\brands.json");
					// Transform To Obj
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsFile);
					// Add To DB & SaveChanges
					if (brands is not null && brands.Any())
					{
						await storeContext.ProductBrands.AddRangeAsync(brands);
						await storeContext.SaveChangesAsync();
					}
				}

				if (!storeContext.ProductTypes.Any())
				{
					// Read As A String
					var typesFile = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\types.json");
					// Transform To Obj
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesFile);
					// Add To DB & SaveChanges
					if (types is not null && types.Any())
					{
						await storeContext.ProductTypes.AddRangeAsync(types);
						await storeContext.SaveChangesAsync();
					}
				}

				if (!storeContext.Products.Any())
				{
					// Read As A String
					var productsFile = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\products.json");
					// Transform To Obj
					var products = JsonSerializer.Deserialize<List<Product>>(productsFile);
					// Add To DB & SaveChanges
					if (products is not null && products.Any())
					{
						await storeContext.Products.AddRangeAsync(products);
						await storeContext.SaveChangesAsync();
					}
				}

			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task InitializerIdentityAsync()
		{

			if (storeIdentityContext.Database.GetPendingMigrations().Any())
				await storeIdentityContext.Database.MigrateAsync();

			if (!roleManager.Roles.Any())
			{
				await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
				await roleManager.CreateAsync(new IdentityRole("Admin"));
			}

			if (!userManager.Users.Any())
			{
				var super = new User()
				{
					DespalyName = "Super Admin",
					UserName = "SuperAdmin",
					Email = "Super@gmail.com",
					PhoneNumber = "1234567890",
				};
				var admin = new User()
				{
					DespalyName = "Admin",
					UserName = "Admin",
					Email = "Admin@gmail.com",
					PhoneNumber = "1234567890",
				};

				await userManager.CreateAsync(super, "Pa$$w0rd");
				await userManager.CreateAsync(admin, "Pa$$w0rd");

				await userManager.AddToRoleAsync(super, "SuperAdmin");
				await userManager.AddToRoleAsync(admin, "Admin");
			}

		}
	}
}
//..\Infrastructure\Persistence\Data\DataSeeding\brands.json

