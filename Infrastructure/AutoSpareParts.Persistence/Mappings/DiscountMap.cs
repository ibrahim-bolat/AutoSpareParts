using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class DiscountMap : BaseEntityMap<Discount>
{
    public override void Configure(EntityTypeBuilder<Discount> builder)
    {
        base.Configure(builder);
        builder.Property(discount => discount.Name).HasMaxLength(250).IsRequired();
        builder.Property(discount => discount.Percent).HasColumnType("decimal(5,4)").IsRequired();
        builder.Property(discount => discount.DiscountDetail).HasMaxLength(1000);
        builder.HasMany(discount => discount.Products).WithMany(product => product.Discounts)
               .UsingEntity(e => e.ToTable("ProductDiscounts"));
    }
}
