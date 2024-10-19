using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
	internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.Property(item => item.Price).HasColumnType("decimal(18,2)");
			builder.OwnsOne(p => p.Product, p => p.WithOwner());
		}
	}
}
