using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasMany(customer => customer.Comments).WithOne(comment => comment.Customer)
            .HasForeignKey(comment => comment.CustomerId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(customer => customer.Orders).WithOne(order => order.Customer)
            .HasForeignKey(order => order.CustomerId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(customer => customer.CustomerAddresses).WithOne(customerAddress => customerAddress.Customer)
            .HasForeignKey(customerAddress => customerAddress.CustomerId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(customer => customer.CustomerImages).WithOne(customerImage => customerImage.Customer)
            .HasForeignKey(customerImage => customerImage.CustomerId).OnDelete(DeleteBehavior.SetNull);

        var hasher = new PasswordHasher<Customer>();
        builder.HasData(new Customer()
        {
            Id = 3,
            FirstName = "Elif Erva",
            LastName = "Bolat",
            UserName = "erva06",
            PhoneNumber = "+90(532)575-79-06",
            NormalizedUserName = "ERVA06",
            Email = "erva06@hotmail.com",
            NormalizedEmail = "ERVA06@HOTMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Ankara.06"),//SecurityStamp = GenerateSecurityStamp(), bu þekilde oluþturulabilir ama her migrationda yenisi oluþur o yüzden sabit deðer verilmeli
            SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA"
        }, new Customer()
        {
            Id = 4,
            FirstName = "Ahmet Enes",
            LastName = "Bolat",
            UserName = "enes06",
            PhoneNumber = "+90(532)575-79-65",
            NormalizedUserName = "ENES06",
            Email = "enes06@hotmail.com",
            NormalizedEmail = "ENES06@HOTMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Ankara.06"),//SecurityStamp = GenerateSecurityStamp(), bu þekilde oluþturulabilir ama her migrationda yenisi oluþur o yüzden sabit deðer verilmeli
            SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA"
        });
    }
}
