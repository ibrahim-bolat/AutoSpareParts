using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;

public class IdentityAppUserMap : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("IdentityUsers").UseTptMappingStrategy();// Table Per Type (TPT) Davranışı Uygulanacak
        builder.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(user => user.LastName).HasMaxLength(100).IsRequired();
        builder.Property(user => user.IdendityNo).HasMaxLength(11);
        builder.Property(user => user.DateOfBirth).IsRequired(false);
        builder.Property(user => user.GenderType).HasConversion( a => a.ToString(), a => (GenderType)Enum.Parse(typeof(GenderType), a));
        builder.Property(user => user.Note).HasMaxLength(500);

        builder.HasMany(user => user.RequestInfoLogs).WithOne(requestInfoLog => requestInfoLog.AppUser)
            .HasForeignKey(requestInfoLog => requestInfoLog.UserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
    }
}
