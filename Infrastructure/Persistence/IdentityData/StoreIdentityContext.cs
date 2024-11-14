
namespace Persistence.IdentityData
{
	public class StoreIdentityContext : IdentityDbContext<User>
	{
		public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Address>().ToTable("Address");
		}
	}
}
