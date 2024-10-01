
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;


public class ModelMap : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(model => model.Id);
        builder.Property(model => model.Id).ValueGeneratedOnAdd();
        builder.Property(model => model.Name).HasMaxLength(500).IsRequired();
        builder.Property(model => model.EngineType).HasMaxLength(250).IsRequired();

        builder.Property(ad => ad.EngineCapacity)
            .HasConversion(
                a => a.ToString(),
                a => (EngineCapacityType)Enum.Parse(typeof(EngineCapacityType), a)).IsRequired();
        builder.Property(ad => ad.EnginePower)
            .HasConversion(
                a => a.ToString(),
                a => (EnginePowerType)Enum.Parse(typeof(EnginePowerType), a)).IsRequired();
        builder.Property(model => model.EquipmentVariant).HasMaxLength(100).IsRequired();
        builder.Property(model => model.ModelYear).HasMaxLength(4).IsFixedLength().IsRequired();

        builder.Property(ad => ad.FuelType)
            .HasConversion(
                a => a.ToString(),
                a => (FuelType)Enum.Parse(typeof(FuelType), a)).IsRequired();
        builder.Property(ad => ad.GearType)
            .HasConversion(
                a => a.ToString(),
                a => (GearType)Enum.Parse(typeof(GearType), a)).IsRequired();
        builder.Property(ad => ad.BodyType)
            .HasConversion(
                a => a.ToString(),
                a => (BodyType)Enum.Parse(typeof(BodyType), a)).IsRequired();
        builder.Property(model => model.Note).HasMaxLength(500);
        builder.HasOne(model => model.BrandSeries).WithMany(brandSeries => brandSeries.Models)
            .HasForeignKey(model => model.BrandSeriesId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(model => model.Products).WithMany(product => product.Models)
            .UsingEntity(e => e.ToTable("ModelProducts")); ;
        builder.HasData(new Model()
        {
            Id = 1,
            BrandSeriesId = 1,
            Name = "Golf VIII (KD) (2019-?)",
            EngineType = "1.6 TDI",
            EngineCapacity = EngineCapacityType.Cm13011600,
            EnginePower = EnginePowerType.Hp101125,
            EquipmentVariant = "Confortline",
            ModelYear = DateTime.Now.AddDays(-2500).Year,
            FuelType = FuelType.Diesel,
            GearType = GearType.Automatic,
            BodyType = BodyType.Hatchback5Door
        }, new Model()
        {
            Id = 2,
            BrandSeriesId = 2,
            Name = "Jetta V (1K) 2005-2010",
            EngineType = "1.4 TSI",
            EngineCapacity = EngineCapacityType.Cm13011600,
            EnginePower = EnginePowerType.Hp101125,
            EquipmentVariant = "Confortline",
            ModelYear = DateTime.Now.AddDays(-2500).Year,
            FuelType = FuelType.Gasoline,
            GearType = GearType.Automatic,
            BodyType = BodyType.Sedan
        }, new Model()
        {
            Id = 3,
            BrandSeriesId = 3,
            Name = "1.6i DTEC",
            EngineType = "1.6 Düz",
            EngineCapacity = EngineCapacityType.Cm13011600,
            EnginePower = EnginePowerType.Hp101125,
            EquipmentVariant = "Elegance",
            ModelYear = DateTime.Now.AddDays(-2500).Year,
            FuelType = FuelType.Gasoline,
            GearType = GearType.Manuel,
            BodyType = BodyType.Hatchback5Door
        }, new Model()
        {
            Id = 4,
            BrandSeriesId = 4,
            Name = "1.6 Multijet Urban",
            EngineType = "1.6 Dizel",
            EngineCapacity = EngineCapacityType.Cm13011600,
            EnginePower = EnginePowerType.Hp101125,
            EquipmentVariant = "Urban",
            ModelYear = DateTime.Now.AddDays(-2500).Year,
            FuelType = FuelType.Diesel,
            GearType = GearType.Automatic,
            BodyType = BodyType.Sedan
        });
    }
}
