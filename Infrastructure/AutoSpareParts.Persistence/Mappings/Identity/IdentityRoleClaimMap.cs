

using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;


public class IdentityRoleClaimMap : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        builder.ToTable("IdentityRoleClaims");
    }
}
