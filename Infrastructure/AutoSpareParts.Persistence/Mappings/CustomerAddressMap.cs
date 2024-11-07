using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class CustomerAddressMap : BaseEntityMap<CustomerAddress>
{
    public override void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        base.Configure(builder);
        builder.Property(customerAddress => customerAddress.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(customerAddress => customerAddress.LastName).HasMaxLength(100).IsRequired();
        builder.Property(customerAddress => customerAddress.Email).HasMaxLength(100).IsRequired();
        builder.Property(customerAddress => customerAddress.PhoneNumber).HasMaxLength(17).IsRequired();
        builder.Property(customerAddress => customerAddress.AddressTitle).HasMaxLength(100).IsRequired();
        builder.Property(customerAddress => customerAddress.AddressType).HasConversion(a => a.ToString(), a => (AddressType)Enum.Parse(typeof(AddressType), a)).IsRequired();
        builder.Property(customerAddress => customerAddress.CityId).HasMaxLength(10).IsRequired();
        builder.Property(customerAddress => customerAddress.CityName).HasMaxLength(250).IsRequired();
        builder.Property(customerAddress => customerAddress.DistrictId).HasMaxLength(10).IsRequired();
        builder.Property(customerAddress => customerAddress.DistrictName).HasMaxLength(250).IsRequired();
        builder.Property(customerAddress => customerAddress.NeighborhoodOrVillageId).HasMaxLength(10).IsRequired();
        builder.Property(customerAddress => customerAddress.NeighborhoodOrVillageName).HasMaxLength(500).IsRequired();
        builder.Property(customerAddress => customerAddress.StreetId).HasMaxLength(10);
        builder.Property(customerAddress => customerAddress.StreetName).HasMaxLength(500);
        builder.Property(customerAddress => customerAddress.PostalCode).HasMaxLength(5);
        builder.Property(customerAddress => customerAddress.AddressDetails).HasMaxLength(500).IsRequired();

        builder.HasOne(customerAddress => customerAddress.Customer).WithMany(customer => customer.CustomerAddresses)
            .HasForeignKey(customerAddress => customerAddress.CustomerId).OnDelete(DeleteBehavior.SetNull);
    }
}
