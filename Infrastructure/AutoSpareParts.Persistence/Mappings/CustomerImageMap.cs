using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class CustomerImageMap : BaseEntityMap<CustomerImage>
{
    public override void Configure(EntityTypeBuilder<CustomerImage> builder)
    {
        base.Configure(builder);
        builder.Property(customerImage => customerImage.Title).HasMaxLength(100).IsRequired();
        builder.Property(customerImage => customerImage.Path).HasMaxLength(500).IsRequired();
        builder.Property(customerImage => customerImage.AltText).HasMaxLength(250);
        builder.Property(customerImage => customerImage.UserImageOrder).ValueGeneratedOnAdd();
        //builder.HasIndex(customerImage => customerImage.UserImageOrder).IsUnique();

        builder.HasOne(customerImage => customerImage.Customer).WithMany(customer => customer.CustomerImages)
                .HasForeignKey(customerImage => customerImage.CustomerId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new CustomerImage()
        {
            Id = 1,
            Title = "ProfilResmi",
            Path = "/images/customerimages/3/profil.jpg",
            AltText = "Profil",
            Profil = true,
            IsActive = true,
            IsDeleted = false,
            CustomerId = 3
        });
    }
}
