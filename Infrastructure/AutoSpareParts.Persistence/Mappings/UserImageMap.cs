using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public sealed class UserImageMap:BaseEntityMap<UserImage>
    {
        public override void Configure(EntityTypeBuilder<UserImage> builder)
        {
            base.Configure(builder);
            builder.Property(userImage => userImage.Title).HasMaxLength(100).IsRequired();
            builder.Property(userImage => userImage.Path).HasMaxLength(500).IsRequired();
            builder.Property(userImage => userImage.AltText).HasMaxLength(250);
            builder.HasOne(userImage => userImage.AppUser).WithMany(user => user.UserImages)
                .HasForeignKey(userImage => userImage.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new UserImage()
            {
                Id = 1, 
                Title = "ProfilResmi",
                Path ="/admin/images/userimages/1/profil.jpg",
                AltText = "Profil",
                Profil = true,
                UserId =1,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
