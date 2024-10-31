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
        builder.HasKey(orderDetail => new { orderDetail.ProductId , orderDetail.OrderId});
        builder.Property(orderDetail => orderDetail.UnitPrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(orderDetail => orderDetail.Quantity).IsRequired();
        builder.Property(orderDetail => orderDetail.Percent).HasColumnType("decimal(5,4)");
        builder.Property(orderDetail => orderDetail.Note).HasMaxLength(500);
        builder.HasOne(orderDetail => orderDetail.Order).WithMany(order => order.OrderDetails)
            .HasForeignKey(orderDetail => orderDetail.OrderId).OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(orderDetail => orderDetail.Product).WithMany(product => product.OrderDetails)
            .HasForeignKey(orderDetail => orderDetail.ProductId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new OrderDetail()
        {
            OrderId =1,
            ProductId = 1,
            UnitPrice = 450000.50m,
            Quantity = 5,
            Percent = 20.00m
        }, new OrderDetail()
        {
            OrderId = 2,
            ProductId = 2,
            UnitPrice = 500000.50m,
            Quantity = 10,
            Percent = 15.00m
        }, new OrderDetail()
        {
            OrderId = 3,
            ProductId = 3,
            UnitPrice = 550000.50m,
            Quantity = 8,
            Percent = 12.00m
        }, new OrderDetail()
        {
            OrderId = 4,
            ProductId = 4,
            UnitPrice = 600000.50m,
            Quantity = 6,
            Percent = 10.00m
        });
    }
}
