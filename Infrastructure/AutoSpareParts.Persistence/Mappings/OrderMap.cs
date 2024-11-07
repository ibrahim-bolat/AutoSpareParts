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
        builder.Property(order => order.OrderCode).HasMaxLength(50).IsRequired();
        builder.Property(order => order.OrderStatus).HasConversion(a => a.ToString(), a => (OrderStatus)Enum.Parse(typeof(OrderStatus), a)).IsRequired();
        builder.Property(order => order.TotalProductQuantity).IsRequired();

        builder.HasOne(order => order.Employee).WithMany(employee => employee.Orders)
            .HasForeignKey(order => order.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(order => order.Customer).WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId).OnDelete(DeleteBehavior.SetNull);        
        
        builder.HasOne(order => order.Payment).WithOne(payment => payment.Order)
           .HasForeignKey<Payment>(payment => payment.Id).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(order => order.OrderDetails).WithOne(orderDetail => orderDetail.Order)
            .HasForeignKey(orderDetail => orderDetail.OrderId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Order()
        {
            Id = 1,
            OrderCode = Guid.NewGuid().ToString(),
            OrderStatus= OrderStatus.Sent,
            TotalProductQuantity=5,
            EmployeeId = 1,
            CustomerId = 3,
        }, new Order()
        {
            Id = 2,
            OrderCode = Guid.NewGuid().ToString(),
            OrderStatus = OrderStatus.NotSent,
            TotalProductQuantity = 10,
            EmployeeId = 1,
            CustomerId = 4,
        }, new Order()
        {
            Id = 3,
            OrderCode = Guid.NewGuid().ToString(),
            OrderStatus = OrderStatus.Canceled,
            TotalProductQuantity = 7,
            EmployeeId = 2,
            CustomerId = 3,
        }, new Order()
        {
            Id = 4,
            OrderCode = Guid.NewGuid().ToString(),
            OrderStatus = OrderStatus.Sent,
            TotalProductQuantity = 3,
            EmployeeId = 2,
            CustomerId = 4,
        });
    }
}
