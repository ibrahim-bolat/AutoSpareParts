using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;
public class IdentityUserRoleMap: IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.ToTable("IdentityUserRoles");
            builder.HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });
        }
    }