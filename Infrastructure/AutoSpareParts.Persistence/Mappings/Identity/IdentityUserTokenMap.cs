

using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;


public class IdentityUserTokenMap : IEntityTypeConfiguration<IdentityUserToken<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder.ToTable("IdentityUserTokens");
    }
}
