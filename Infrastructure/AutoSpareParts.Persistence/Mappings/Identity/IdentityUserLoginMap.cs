

using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;


public class IdentityUserLoginMap : IEntityTypeConfiguration<IdentityUserLogin<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder.ToTable("IdentityUserLogins");
    }
}
