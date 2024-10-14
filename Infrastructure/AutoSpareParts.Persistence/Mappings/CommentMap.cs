using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class CommentMap : BaseEntityMap<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);
        builder.Property(comment => comment.CommentDetail).HasMaxLength(1000).IsRequired();
        builder.Property(comment => comment.CommentOrder).IsRequired();
        builder.Property(comment => comment.CommentStarRating).HasDefaultValue(0);
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
