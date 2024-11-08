using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class EmployeeMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(employee => employee.Title).HasMaxLength(100).IsRequired();
        builder.Property(employee => employee.HireDate).IsRequired();

        builder.HasMany(employee => employee.Ads).WithOne(ad => ad.Employee)
             .HasForeignKey(ad => ad.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(employee => employee.Orders).WithOne(order => order.Employee)
            .HasForeignKey(order => order.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(employee => employee.EmployeeAddresses).WithOne(employeeAddress => employeeAddress.Employee)
             .HasForeignKey(employeeAddress => employeeAddress.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(employee => employee.EmployeeImages).WithOne(employeeImage => employeeImage.Employee)
            .HasForeignKey(employeeImage => employeeImage.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        var hasher = new PasswordHasher<Employee>();
        builder.HasData(new Employee()
        {
            Id = 1,
            FirstName = "Ýbrahim",
            LastName = "Bolat",
            UserName = "bolat6606",
            PhoneNumber = "+90(532)575-79-66",
            NormalizedUserName = "BOLAT6606",
            Email = "bolat6606@hotmail.com",
            NormalizedEmail = "BOLAT6606@HOTMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Ankara.06"),//SecurityStamp = GenerateSecurityStamp(), bu þekilde oluþturulabilir ama her migrationda yenisi oluþur o yüzden sabit deðer verilmeli
            SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
            Title = "Sales Representative",
            HireDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20))
        }, new Employee()
        {
            Id = 2,
            FirstName = "Beril Nisa",
            LastName = "Bolat",
            UserName = "beril06",
            PhoneNumber = "+90(532)575-79-60",
            NormalizedUserName = "BERIL06",
            Email = "beril06@hotmail.com",
            NormalizedEmail = "BERIL06@HOTMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Ankara.06"),//SecurityStamp = GenerateSecurityStamp(), bu þekilde oluþturulabilir ama her migrationda yenisi oluþur o yüzden sabit deðer verilmeli
            SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
            Title = "Director",
            HireDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10))
        });
    }
}
