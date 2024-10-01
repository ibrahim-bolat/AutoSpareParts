

using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;


public class IdentityAppRoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("IdentityRoles");
            builder.Property(role => role.Note).HasMaxLength(500);
            builder.HasMany(role => role.Endpoints).WithMany(endpoint => endpoint.AppRoles)
                .UsingEntity(role => role.ToTable("EndpointRoles"));
            builder.HasData(new AppRole
            {
                Id = 1,
                Name = RoleType.Owner.ToString(),
                NormalizedName = RoleType.Owner.ToString().ToUpperInvariant()
            },
            new AppRole
            {
                Id = 2,
                Name = RoleType.Admin.ToString(),
                NormalizedName = RoleType.Admin.ToString().ToUpperInvariant()
            },
            new AppRole
            {
                Id = 3,
                Name = RoleType.User.ToString(),
                NormalizedName = RoleType.User.ToString().ToUpperInvariant()
            });
         }
    }
