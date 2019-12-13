using _3M.DataModels.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels.Configuration
{
    public class ShoppingItemConfiguration : IEntityTypeConfiguration<ShoppingItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.Property(i => i.ProductPrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.ItemPrice).HasColumnType("decimal(18,2)");
            builder.Ignore(i => i.ItemPrice);

            builder.ToTable(typeof(ShoppingItem).Name);
        }
    }
}
