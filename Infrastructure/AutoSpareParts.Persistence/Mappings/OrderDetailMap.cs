using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class OrderDetailMap : BaseEntityMap<OrderDetail>
{
    public override void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        base.Configure(builder);
        builder.HasIndex(orderDetail => new { orderDetail.ProductId, orderDetail.OrderId }).IsUnique();// bu iki sütun tekil olmalý
        builder.Property(orderDetail => orderDetail.UnitPrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(orderDetail => orderDetail.DiscountPercent).HasColumnType("decimal(5,2)");
        builder.Property(orderDetail => orderDetail.Quantity).IsRequired();

        builder.HasOne(orderDetail => orderDetail.Order).WithMany(order => order.OrderDetails)
            .HasForeignKey(orderDetail => orderDetail.OrderId).OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(orderDetail => orderDetail.Product).WithMany(product => product.OrderDetails)
            .HasForeignKey(orderDetail => orderDetail.ProductId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new OrderDetail()
        {
            Id=1,
            OrderId =1,
            ProductId = 1,
            UnitPrice = 450000.50m,
            DiscountPercent = 20.00m,
            Quantity = 5
        }, new OrderDetail()
        {
            Id = 2,
            OrderId = 2,
            ProductId = 2,
            UnitPrice = 500000.50m,
            DiscountPercent = 15.00m,
            Quantity = 10
        }, new OrderDetail()
        {
            Id = 3,
            OrderId = 3,
            ProductId = 3,
            UnitPrice = 550000.50m,
            DiscountPercent = 12.00m,
            Quantity = 8
        }, new OrderDetail()
        {
            Id = 4,
            OrderId = 4,
            ProductId = 4,
            UnitPrice = 600000.50m,
            DiscountPercent = 150.75m,
            Quantity = 6
        });
    }
}
