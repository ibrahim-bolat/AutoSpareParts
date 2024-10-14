using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class AddressMap : BaseEntityMap<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);
        builder.Property(address => address.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(address => address.LastName).HasMaxLength(100).IsRequired();
        builder.Property(address => address.Email).HasMaxLength(100).IsRequired();
        builder.Property(address => address.PhoneNumber).HasMaxLength(17).IsRequired();
        builder.Property(address => address.AddressTitle).HasMaxLength(100).IsRequired();
        builder.Property(address => address.AddressType)
            .HasConversion(
                a => a.ToString(),
                a => (AddressType)Enum.Parse(typeof(AddressType), a))
            .IsRequired();
        builder.Property(address => address.CityId).HasMaxLength(10).IsRequired();
        builder.Property(address => address.CityName).HasMaxLength(250).IsRequired();
        builder.Property(address => address.DistrictId).HasMaxLength(10).IsRequired();
        builder.Property(address => address.DistrictName).HasMaxLength(250).IsRequired();
        builder.Property(address => address.NeighborhoodOrVillageId).HasMaxLength(10).IsRequired();
        builder.Property(address => address.NeighborhoodOrVillageName).HasMaxLength(500).IsRequired();
        builder.Property(address => address.StreetId).HasMaxLength(10);
        builder.Property(address => address.StreetName).HasMaxLength(500);
        builder.Property(address => address.PostalCode).HasMaxLength(5);
        builder.Property(address => address.AddressDetails).HasMaxLength(500).IsRequired();
        builder.HasOne(address => address.AppUser).WithMany(user => user.Addresses)
            .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
