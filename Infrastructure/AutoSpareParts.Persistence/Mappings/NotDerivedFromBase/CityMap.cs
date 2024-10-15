using AutoSpareParts.Domain.Entities.NotDerivedFromBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.NotDerivedFromBase;

public sealed class CityMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(city => city.Id);
        builder.Property(city => city.Id).ValueGeneratedOnAdd();
        builder.Property(city => city.Name).HasMaxLength(250).IsRequired();
        builder.HasMany(city => city.Districts).WithOne(district => district.City)
            .HasForeignKey(district => district.CityId).OnDelete(DeleteBehavior.Cascade);
    }
}
