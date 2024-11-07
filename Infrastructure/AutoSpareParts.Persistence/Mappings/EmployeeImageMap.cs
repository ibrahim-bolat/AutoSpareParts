using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class EmployeeImageMap : BaseEntityMap<EmployeeImage>
{
    public override void Configure(EntityTypeBuilder<EmployeeImage> builder)
    {
        base.Configure(builder);
        builder.Property(employeeImage => employeeImage.Title).HasMaxLength(100).IsRequired();
        builder.Property(employeeImage => employeeImage.Path).HasMaxLength(500).IsRequired();
        builder.Property(employeeImage => employeeImage.AltText).HasMaxLength(250);
        builder.Property(employeeImage => employeeImage.UserImageOrder).ValueGeneratedOnAdd();
        //builder.HasIndex(employeeImage => employeeImage.UserImageOrder).IsUnique();

        builder.HasOne(employeeImage => employeeImage.Employee).WithMany(employee => employee.EmployeeImages)
                .HasForeignKey(employeeImage => employeeImage.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new EmployeeImage()
        {
            Id = 1,
            Title = "ProfilResmi",
            Path = "/admin/images/userimages/1/profil.jpg",
            AltText = "Profil",
            Profil = true,
            EmployeeId = 1,
            IsActive = true,
            IsDeleted = false
        });
    }
}
