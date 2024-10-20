using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
	internal class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethods>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethods> builder)
		{
			builder.Property(d=>d.Cost).HasColumnType("decimal(18,2)");
		}
	}
}
