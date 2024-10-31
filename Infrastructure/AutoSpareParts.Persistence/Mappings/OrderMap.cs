using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class OrderMap : BaseEntityMap<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        builder.Property(order => order.TotalProductQuantity).IsRequired();
        builder.HasOne(order => order.Payment).WithOne(payment => payment.Order)
           .HasForeignKey<Payment>(payment => payment.Id).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(order => order.AppUser).WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId).OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(order => order.OrderDetails).WithOne(orderDetail => orderDetail.Order)
            .HasForeignKey(orderDetail => orderDetail.OrderId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Order()
        {
            Id = 1,
            UserId =1,
        }, new Order()
        {
            Id = 2,
            UserId = 1,
        }, new Order()
        {
            Id = 3,
            UserId = 2,
        }, new Order()
        {
            Id = 4,
            UserId = 2,
        });
    }
}
