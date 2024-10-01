

using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;


public class IdentityUserClaimMap : IEntityTypeConfiguration<IdentityUserClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
    {
        builder.ToTable("IdentityUserClaims");
    }
}
