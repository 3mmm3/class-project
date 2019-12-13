using _3M.DataModels.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3M.DataModels.Configuration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.ToTable("Property");
            //    .HasDiscriminator<int>("FeatureType")
            //    .HasValue<BoolFeature>(1)
            //    .HasValue<IntFeature>(2)
            //    .HasValue<DoubleFeature>(3)
            //    .HasValue<StringFeature>(4);
        }
    }
}
