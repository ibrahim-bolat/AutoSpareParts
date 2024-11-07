using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class IPAddressMap : BaseEntityMap<IPAddress>
{
    public override void Configure(EntityTypeBuilder<IPAddress> builder)
    {
        base.Configure(builder);
        builder.Property(ip => ip.RangeStart).HasMaxLength(100).IsRequired();
        builder.Property(ip => ip.RangeEnd).HasMaxLength(17).IsRequired();
        builder.Property(ip => ip.IPListType).HasConversion(a => a.ToString(), a => (IPListType)Enum.Parse(typeof(IPListType), a)).IsRequired();

        builder.HasMany(ip => ip.Endpoints).WithMany(endpoint => endpoint.IPAddresses)
            .UsingEntity(e => e.ToTable("EndpointIPAddreses"));

        builder.HasData(new IPAddress()
        {
            Id = 1,
            RangeStart = "192.168.10.30",
            RangeEnd = "192.168.10.50",
            IPListType = IPListType.BlackList,
        },
        new IPAddress()
         {
            Id = 2,
            RangeStart = "192.168.0.10",
            RangeEnd = "192.168.10.20",
            IPListType = IPListType.WhiteList,
        });

    }
}
