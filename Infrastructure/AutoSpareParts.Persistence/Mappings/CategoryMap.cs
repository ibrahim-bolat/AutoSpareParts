using AutoSpareParts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);
        builder.Property(category => category.Id).ValueGeneratedOnAdd();
        builder.Property(category => category.Name).HasMaxLength(250).IsRequired();
        builder.HasOne(category => category.MainCategory).WithMany(mainCategory => mainCategory.Categories)
            .HasForeignKey(category => category.MainCategoryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(category => category.Products).WithMany(ad => ad.Categories)
        .UsingEntity(e => e.ToTable("AdCategories"));
        builder.HasData(new Category()
        {
            Id = 1,
            MainCategoryId = 1,
            Name = "Abs Sensörü"
        }, new Category()
        {
            Id = 2,
            MainCategoryId = 1,
            Name = "Balata"
        }, new Category()
        {
            Id = 3,
            MainCategoryId = 2,
            Name = "Volan"
        }, new Category()
        {
            Id = 4,
            MainCategoryId = 2,
            Name = "Debriyaj Rulmaný"
        });

    }
}
