using AutoSpareParts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public class BrandSeriesMap : IEntityTypeConfiguration<BrandSeries>
{
    public void Configure(EntityTypeBuilder<BrandSeries> builder)
    {
        builder.HasKey(brandSeries => brandSeries.Id);
        builder.Property(brandSeries => brandSeries.Id).ValueGeneratedOnAdd();
        builder.Property(brandSeries => brandSeries.Name).HasMaxLength(250).IsRequired();
        builder.Property(brandSeries => brandSeries.Note).HasMaxLength(500);
        builder.HasOne(brandSeries => brandSeries.Brand).WithMany(brand => brand.BrandSeries)
            .HasForeignKey(brandSeries => brandSeries.BrandId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(brandSeries => brandSeries.Models).WithOne(model => model.BrandSeries)
            .HasForeignKey(brandSeries => brandSeries.BrandSeriesId).OnDelete(DeleteBehavior.Cascade);
        builder.HasData(new BrandSeries()
        {
            Id = 1,
            BrandId = 1,
            Name = "Golf"
        }, new BrandSeries()
        {
            Id = 2,
            BrandId = 1,
            Name = "Jetta"
        }, new BrandSeries()
        {
            Id = 3,
            BrandId = 2,
            Name = "Civic"
        }, new BrandSeries()
        {
            Id = 4,
            BrandId = 3,
            Name = "Egea"
        });

    }
}
