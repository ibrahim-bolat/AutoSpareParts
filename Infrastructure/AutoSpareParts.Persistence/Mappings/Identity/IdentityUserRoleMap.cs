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
                UserId = 1,
                RoleId = 1
            },new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 2
            },new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 4
            },new IdentityUserRole<int>
            {
                UserId = 3,
                RoleId = 5
            });
        }
    }