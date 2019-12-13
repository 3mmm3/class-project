using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _3M.DataModels.Configuration;
using _3M.DataModels.Sales;

namespace _3M.DataModels.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.Property(i => i.TotalPrice).HasColumnType("decimal(18,2)");

            builder.ToTable(typeof(Bill).Name);
        }
    }
}
