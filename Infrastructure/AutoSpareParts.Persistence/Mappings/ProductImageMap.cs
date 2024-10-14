using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public sealed class ProductImageMap : BaseEntityMap<ProductImage>
    {
        public override void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            base.Configure(builder);
            builder.Property(productImage => productImage.Title).HasMaxLength(250).IsRequired();
            builder.Property(productImage => productImage.Path).HasMaxLength(500).IsRequired();
            builder.Property(productImage => productImage.AltText).HasMaxLength(250);
            builder.HasOne(productImage => productImage.Product).WithMany(product => product.ProductImages)
                .HasForeignKey(productImage => productImage.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new ProductImage()
        {
            Id = 1,
            ProductId = 1,
            Title = "Ford Focuk Fren Disk Resmi",
            Path = "/img/products/fren/frendiskleri/1/fordfocusfrendisk.jpg",
            AltText = "Ford Focuk Fren Disk Resmi",
            Vitrin = true,
        }, new ProductImage()
        {
            Id = 2,
            ProductId = 3,
            Title = "Fiat Linea 1.3 Fren Diski Resmi",
            Path = "/img/products/fren/frendiskleri/2/fiatlineafrendisk.jpg",
            AltText = "Ford Focuk Fren Disk Resmi",
            Vitrin = true,
        });
        }
    }
