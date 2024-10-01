using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id).ValueGeneratedOnAdd();
        builder.Property(product => product.Name).HasMaxLength(250).IsRequired();
        builder.Property(product => product.StockCode).HasMaxLength(250).IsRequired();
        builder.Property(product => product.StockStatus).IsRequired();
        builder.Property(product => product.StockQuantity).HasDefaultValue(0).IsRequired();
        builder.Property(product => product.ProductStatus)
            .HasConversion(
                a => a.ToString(),
                a => (ProductStatus)Enum.Parse(typeof(ProductStatus), a)).IsRequired();
        builder.Property(product => product.GuaranteeStatus)
            .HasConversion(
                a => a.ToString(),
                a => (GuaranteeStatus)Enum.Parse(typeof(GuaranteeStatus), a)).IsRequired();
        builder.Property(product => product.OriginalityStatus)
            .HasConversion(
                a => a.ToString(),
                a => (OriginalityStatus)Enum.Parse(typeof(OriginalityStatus), a)).IsRequired();
        builder.Property(product => product.PurchasePrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(product => product.SalePrice).HasColumnType("decimal(18,4)").IsRequired();

        builder.Property(product => product.ProductDetail).HasMaxLength(1000);
        builder.Property(product => product.Note).HasMaxLength(500);
        builder.HasMany(product => product.Categories).WithMany(category => category.Products)
            .UsingEntity(e => e.ToTable("CategoryProducts")); ;

        builder.HasMany(product => product.Models).WithMany(model => model.Products)
            .UsingEntity(e => e.ToTable("ModelProducts")); ;

        builder.HasMany(product => product.Oems).WithOne(oem => oem.Product)
            .HasForeignKey(oem => oem.ProductId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(product => product.ProductImages).WithOne(productImages => productImages.Product)
            .HasForeignKey(productImage => productImage.ProductId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(product => product.Ads).WithOne(ad => ad.Product)
            .HasForeignKey(ad => ad.ProductId).OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new Product()
        {
            Id = 1,
            Name = "Ford Focus 1.6 Fren Balatası",
            StockCode = "IVECO5802794866",
            StockStatus = true,
            StockQuantity = 500,
            ProductionDate = DateOnly.FromDateTime(DateTime.Now),
            ProductStatus = ProductStatus.FirstHand,
            GuaranteeStatus = GuaranteeStatus.Yes,
            GuaranteePeriod = 2,
            OriginalityStatus = OriginalityStatus.Original,
            PurchasePrice = 350000.50m,
            SalePrice = 450000.50m,
            ProductDetail = "ÇOK GÜZEL Balata"
        }, new Product()
        {
            Id = 2,
            Name = "Ford Corier 1.5 Debriyaj Seti",
            StockCode = "IVECO5802794845",
            StockStatus = true,
            StockQuantity = 1000,
            ProductionDate = DateOnly.FromDateTime(DateTime.Now),
            ProductStatus = ProductStatus.FirstHand,
            GuaranteeStatus = GuaranteeStatus.Yes,
            GuaranteePeriod = 1,
            OriginalityStatus = OriginalityStatus.SubIndustry,
            PurchasePrice = 360000.50m,
            SalePrice = 460000.50m,
            ProductDetail = "ÇOK GÜZEL debriyaj"
        }, new Product()
        {
            Id = 3,
            Name = "Fiat Linea 1.3 Fren Diski",
            StockCode = "IVECO5802794850",
            StockStatus = true,
            StockQuantity = 1500,
            ProductionDate = DateOnly.FromDateTime(DateTime.Now),
            ProductStatus = ProductStatus.SecondHand,
            GuaranteeStatus = GuaranteeStatus.No,
            GuaranteePeriod = 0,
            OriginalityStatus = OriginalityStatus.Cannibalized,
            PurchasePrice = 340000.50m,
            SalePrice = 440000.50m,
            ProductDetail = "ÇOK GÜZEL Disk"
        }, new Product()
        {
            Id = 4,
            Name = "Opel Astra 1.6 CDTI Fren Balatası",
            StockCode = "IVECO5802794870",
            StockStatus = true,
            StockQuantity = 2000,
            ProductionDate = DateOnly.FromDateTime(DateTime.Now),
            ProductStatus = ProductStatus.FirstHand,
            GuaranteeStatus = GuaranteeStatus.Yes,
            GuaranteePeriod = 2,
            OriginalityStatus = OriginalityStatus.Original,
            PurchasePrice = 420000.50m,
            SalePrice = 520000.50m,
            ProductDetail = "Mükemmel Balata"
        });
    }
}
