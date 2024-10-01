using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace AutoSpareParts.Persistence.Mappings;

public class AdMap : IEntityTypeConfiguration<Ad>
{
    public void Configure(EntityTypeBuilder<Ad> builder)
    {
        builder.HasKey(ad => ad.Id);
        builder.Property(ad => ad.Id).ValueGeneratedOnAdd();
        builder.Property(ad => ad.AdNo).HasMaxLength(30).IsRequired();
        builder.Property(ad => ad.Title).HasMaxLength(500).IsRequired();
        builder.Property(ad => ad.FormerPrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(ad => ad.CurrentPrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(ad => ad.DiscountAmount).HasMaxLength(100).IsRequired();
        builder.Property(ad => ad.StarRating).HasDefaultValue(0);
        builder.Property(ad => ad.AdPageOrder).IsRequired();
        builder.Property(ad => ad.AdDetail).HasMaxLength(1000);
        builder.Property(ad => ad.Note).HasMaxLength(500);

        builder.HasOne(ad => ad.Product).WithMany(product => product.Ads)
            .HasForeignKey(ad => ad.ProductId).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ad => ad.AppUser).WithMany(user => user.Ads)
            .HasForeignKey(ad => ad.UserId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ad => ad.Comments).WithOne(comment => comment.Ad)
            .HasForeignKey(comment => comment.AdId).OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new Ad()
        {
            Id = 1,
            ProductId = 1,
            UserId = 1,
            AdNo = "111111111",
            Title = "Ford Focus 1.6 Fren Balatası Orijinal",
            AdDate = DateTime.Now,
            FormerPrice = 550000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%25",
            StarRating =5,
            AdPageOrder = 1,
            AdDetail = "FORD FOCUS KASA ARKA FREN DİSKİ RULMANLI 1 ADET FİYATIDIR. MAİS RENAULT ORJİNAL ÜRÜNÜDÜR.",
        }, new Ad()
        {
            Id = 2,
            ProductId = 2,
            UserId= 2,
            AdNo = "11111112",
            Title = "Ford Corier 1.5 Debriyaj Seti Gıcırmı Gıcır",
            AdDate = DateTime.Now,
            FormerPrice = 450000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%0",
            StarRating = 3,
            AdPageOrder = 1,
            AdDetail = "Ford Corier YENİ KASA DEBRİYAJ  SETİ 1 ADET FİYATIDIR. Orijinal BOSCH ÜRÜNÜDÜR.",
        }, new Ad()
        {
            Id = 3,
            ProductId = 3,
            UserId = 1,
            AdNo = "11111113",
            Title = "Fiat Linea 1.3 Fren Diski",
            AdDate = DateTime.Now,
            FormerPrice = 650000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%20",
            StarRating = 4,
            AdPageOrder = 1,
            AdDetail = "Ford Linea YENİ KASA Fren Diski 1 ADET FİYATIDIR. Orijinal BOSCH ÜRÜNÜDÜR.",
        }, new Ad()
        {
            Id = 4,
            ProductId = 4,
            UserId = 2,
            AdNo = "11111114",
            Title = "Opel Astra 1.6 CDTI Fren Balatası",
            AdDate = DateTime.Now,
            FormerPrice = 470000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%5",
            StarRating = 5,
            AdPageOrder = 1,
            AdDetail = "Opel Astra 1.6 CDTI YENİ KASA Fren Balatası 1 ADET FİYATIDIR. Orijinal General Motor ÜRÜNÜDÜR.",
        });
    }
}
