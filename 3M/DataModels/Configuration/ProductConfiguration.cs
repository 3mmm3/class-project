using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
            builder.HasMany(i => i.Properties);

            builder.HasOne(i => i.Category)
                .WithMany();
            builder.ToTable(typeof(Product).Name);
        }
    }
}
