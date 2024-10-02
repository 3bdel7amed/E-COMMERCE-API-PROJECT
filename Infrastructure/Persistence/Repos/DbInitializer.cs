using Persistence.Data;
using System;
using System.Globalization;

namespace Persistence.Repos
{
	public class DbInitializer : IDbInitializer
	{
		readonly StoreContext storeContext;
		public DbInitializer(StoreContext _storeContext) => storeContext = _storeContext;
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
	}
}
//..\Infrastructure\Persistence\Data\DataSeeding\brands.json

