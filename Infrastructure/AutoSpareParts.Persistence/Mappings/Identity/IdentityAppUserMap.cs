using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Identity;

    public class IdentityAppUserMap:IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("IdentityUsers");
            builder.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(100).IsRequired();
            builder.Property(user => user.UserIdendityNo).HasMaxLength(11);
            builder.Property(user => user.DateOfBirth).IsRequired(false);
            builder.Property(user => user.GenderType)
                .HasConversion(
                    a => a.ToString(),
                    a => (GenderType)Enum.Parse(typeof(GenderType), a));
            builder.Property(user => user.Note).HasMaxLength(500);
            builder.HasMany(user => user.Ads).WithOne(ad => ad.AppUser)
                .HasForeignKey(ad => ad.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.Addresses).WithOne(address => address.AppUser)
                .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.UserImages).WithOne(userImage => userImage.AppUser)
                .HasForeignKey(userImage => userImage.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.RequestInfoLogs).WithOne(requestInfoLog => requestInfoLog.AppUser)
                .HasForeignKey(requestInfoLog => requestInfoLog.UserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.Comments).WithOne(comment => comment.AppUser)
                .HasForeignKey(comment => comment.UserId).OnDelete(DeleteBehavior.Cascade);

        var hasher = new PasswordHasher<AppUser>();
            builder.HasData(new AppUser
            {
                Id = 1,
                FirstName = "İbrahim",
                LastName = "Bolat",
                UserName = "bolat6606",
                PhoneNumber = "+90(532)575-79-66",
                NormalizedUserName = "BOLAT6606",
                Email = "bolat6606@hotmail.com",
                NormalizedEmail = "BOLAT6606@HOTMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Ankara.06"),
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                //SecurityStamp = GenerateSecurityStamp(), bu şekilde oluşturulabilir ama 
                //her migrationda yenisi oluşur o yüzden sabit değer verilmeli
            }, new AppUser
            {
                Id = 2,
                FirstName = "Beril Nisa",
                LastName = "Bolat",
                UserName = "beril06",
                PhoneNumber = "+90(532)575-79-60",
                NormalizedUserName = "BERIL06",
                Email = "beril06@hotmail.com",
                NormalizedEmail = "BERIL06@HOTMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Ankara.06"),
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                //SecurityStamp = GenerateSecurityStamp(), bu şekilde oluşturulabilir ama 
                //her migrationda yenisi oluşur o yüzden sabit değer verilmeli
            });
    }
    }
