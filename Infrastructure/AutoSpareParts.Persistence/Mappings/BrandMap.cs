using AutoSpareParts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public class BrandMap:IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(brand => brand.Id);
            builder.Property(brand => brand.Id).ValueGeneratedOnAdd();
            builder.Property(brand => brand.Name).HasMaxLength(250).IsRequired();
            builder.Property(brand => brand.Note).HasMaxLength(500);
            builder.HasMany(brand => brand.BrandSeries).WithOne(brandSeries => brandSeries.Brand)
                .HasForeignKey(brandSeries => brandSeries.BrandId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new Brand()
            {
                Id = 1, 
                Name = "Wolkswagen"
            }, new Brand()
            {
                Id = 2,
                Name = "Honda"
            }, new Brand()
            {
                Id = 3,
                Name = "Fiat"
            }, new Brand()
            {
                Id = 4,
                Name = "Nissan"
            });

        }
    }
