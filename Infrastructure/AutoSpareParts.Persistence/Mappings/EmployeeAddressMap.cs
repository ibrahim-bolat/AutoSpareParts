using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class EmployeeAddressMap : BaseEntityMap<EmployeeAddress>
{
    public override void Configure(EntityTypeBuilder<EmployeeAddress> builder)
    {
        base.Configure(builder);
        builder.Property(employeeAddress => employeeAddress.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(employeeAddress => employeeAddress.LastName).HasMaxLength(100).IsRequired();
        builder.Property(employeeAddress => employeeAddress.Email).HasMaxLength(100).IsRequired();
        builder.Property(employeeAddress => employeeAddress.PhoneNumber).HasMaxLength(17).IsRequired();
        builder.Property(employeeAddress => employeeAddress.AddressTitle).HasMaxLength(100).IsRequired();
        builder.Property(employeeAddress => employeeAddress.AddressType).HasConversion(a => a.ToString(), a => (AddressType)Enum.Parse(typeof(AddressType), a)).IsRequired();
        builder.Property(employeeAddress => employeeAddress.CityId).HasMaxLength(10).IsRequired();
        builder.Property(employeeAddress => employeeAddress.CityName).HasMaxLength(250).IsRequired();
        builder.Property(employeeAddress => employeeAddress.DistrictId).HasMaxLength(10).IsRequired();
        builder.Property(employeeAddress => employeeAddress.DistrictName).HasMaxLength(250).IsRequired();
        builder.Property(employeeAddress => employeeAddress.NeighborhoodOrVillageId).HasMaxLength(10).IsRequired();
        builder.Property(employeeAddress => employeeAddress.NeighborhoodOrVillageName).HasMaxLength(500).IsRequired();
        builder.Property(employeeAddress => employeeAddress.StreetId).HasMaxLength(10);
        builder.Property(employeeAddress => employeeAddress.StreetName).HasMaxLength(500);
        builder.Property(employeeAddress => employeeAddress.PostalCode).HasMaxLength(5);
        builder.Property(employeeAddress => employeeAddress.AddressDetails).HasMaxLength(500).IsRequired();

        builder.HasOne(employeeAddress => employeeAddress.Employee).WithMany(employee => employee.EmployeeAddresses)
            .HasForeignKey(employeeAddress => employeeAddress.EmployeeId).OnDelete(DeleteBehavior.SetNull);
    }
}
