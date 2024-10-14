using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public sealed class IpAddressMap:BaseEntityMap<IpAddress>
    {
        public override void Configure(EntityTypeBuilder<IpAddress> builder)
        {
            base.Configure(builder);
            builder.Property(ip => ip.RangeStart).HasMaxLength(100).IsRequired();
            builder.Property(ip => ip.RangeEnd).HasMaxLength(17).IsRequired();
            builder.Property(ip => ip.IpListType)
                .HasConversion(
                    a=>a.ToString(),
                    a=>(IpListType)Enum.Parse(typeof(IpListType),a))
                .IsRequired();
            builder.HasMany(ip => ip.Endpoints).WithMany(endpoint => endpoint.IpAddresses)
                .UsingEntity(e => e.ToTable("EndpointIpAddreses"));
            builder.HasData(new IpAddress()
            {
                Id = 1, 
                RangeStart = "192.168.10.30",
                RangeEnd = "192.168.10.50",
                IpListType = IpListType.BlackList,
            },
                new IpAddress()
                {
                    Id = 2, 
                    RangeStart = "192.168.0.10",
                    RangeEnd = "192.168.10.20",
                    IpListType = IpListType.WhiteList,
                });

        }
    }
