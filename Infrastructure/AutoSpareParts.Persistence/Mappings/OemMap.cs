using AutoSpareParts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public class OemMap : IEntityTypeConfiguration<Oem>
{
    public void Configure(EntityTypeBuilder<Oem> builder)
    {
        builder.HasKey(oem=> oem.Id);
        builder.Property(oem => oem.Id).ValueGeneratedOnAdd();
        builder.Property(oem => oem.OemNo).HasMaxLength(250).IsRequired();
        builder.Property(oem => oem.OemBrandName).HasMaxLength(250).IsRequired();
        builder.Property(oem => oem.Note).HasMaxLength(500);
        builder.HasOne(oem => oem.Product).WithMany(product => product.Oems)
            .HasForeignKey(oem => oem.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasData(new Oem()
        {
            Id = 1,
            ProductId = 1,
            OemNo = "3140005010",
            OemBrandName="Ford"

        }, new Oem()
        {
            Id = 2,
            ProductId = 1,
            OemNo = "3140005011",
            OemBrandName = "Volvo"
        }, new Oem()
        {
            Id = 3,
            ProductId = 2,
            OemNo = "3140005012",
            OemBrandName = "Ford"
        }, new Oem()
        {
            Id = 4,
            ProductId = 2,
            OemNo = "3140005013",
            OemBrandName = "FordUSA"
        });

    }
}
