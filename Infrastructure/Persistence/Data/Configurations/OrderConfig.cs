using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
	internal class OrderConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(O => O.ShippingAddress,O=>O.WithOwner());

			builder.HasMany(O => O.Items).WithOne();

			builder.Property(O => O.Status).HasConversion(S => S.ToString(), S => Enum.Parse<PaymentStatus>(S));

			builder.HasOne(O => O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

			builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
		}
	}
}
