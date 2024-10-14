using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public sealed class EndpointMap:BaseEntityMap<Endpoint>
    {
        public override void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            base.Configure(builder);
            builder.Property(endpoint => endpoint.Code).HasMaxLength(150).IsRequired();
            builder.Property(endpoint => endpoint.Definition).HasMaxLength(150).IsRequired();
            builder.Property(endpoint => endpoint.HttpType).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.EndpointType).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.EndpointName).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.ControllerName).HasMaxLength(100).IsRequired();
            builder.Property(endpoint => endpoint.AreaName).HasMaxLength(100);
            builder.HasMany(endpoint => endpoint.AppRoles).WithMany(appRole => appRole.Endpoints)
                .UsingEntity(e => e.ToTable("EndpointRoles"));;
            builder.HasMany(endpoint => endpoint.IpAddresses).WithMany(ip => ip.Endpoints)
                .UsingEntity(e => e.ToTable("EndpointIpAddreses"));;
        }
    }
