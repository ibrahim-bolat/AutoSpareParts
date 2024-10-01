using AutoSpareParts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(comment=> comment.Id);
        builder.Property(comment => comment.Id).ValueGeneratedOnAdd();
        builder.Property(comment => comment.CommentDetail).HasMaxLength(1000).IsRequired();
        builder.Property(comment => comment.CommentOrder).IsRequired();
        builder.Property(comment => comment.CommentStarRating).HasDefaultValue(0);
        builder.Property(comment => comment.Note).HasMaxLength(500);
        builder.HasOne(comment => comment.Ad).WithMany(ad => ad.Comments)
            .HasForeignKey(comment => comment.AdId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(comment => comment.AppUser).WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasData(new Comment()
        {
            Id = 1,
            UserId=1,
            AdId = 1,
            CommentDetail = "Çok Beðendim",
            CommentOrder = 1,
            CommentStarRating = 5,

        }, new Comment()
        {
            Id = 2,
            UserId = 2,
            AdId = 1,
            CommentDetail = "Beðendim",
            CommentOrder = 2,
            CommentStarRating = 4,
        }, new Comment()
        {
            Id = 3,
            UserId = 1,
            AdId = 2,
            CommentDetail = "Çok Beðendim",
            CommentOrder = 1,
            CommentStarRating = 5,
        }, new Comment()
        {
            Id = 4,
            UserId = 2,
            AdId = 2,
            CommentDetail = "Beðendim",
            CommentOrder = 2,
            CommentStarRating = 3,
        });

    }
}
