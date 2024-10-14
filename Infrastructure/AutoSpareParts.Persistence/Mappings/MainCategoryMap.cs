using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class MainCategoryMap : BaseEntityMap<MainCategory>
{
    public override void Configure(EntityTypeBuilder<MainCategory> builder)
    {
        base.Configure(builder);
        builder.HasKey(mainCategory => mainCategory.Id);
        builder.Property(mainCategory => mainCategory.Id).ValueGeneratedOnAdd();
        builder.Property(mainCategory => mainCategory.Name).HasMaxLength(250).IsRequired();
        builder.HasMany(mainCategory => mainCategory.Categories).WithOne(category => category.MainCategory)
            .HasForeignKey(category => category.MainCategoryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasData(new MainCategory()
        {
            Id = 1,
            Name = "Fren"
        },
        new MainCategory()
        {
            Id = 2,
            Name = "Debriyaj"
        });
    }
}
