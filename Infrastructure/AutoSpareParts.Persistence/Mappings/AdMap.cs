using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class AdMap : BaseEntityMap<Ad>
{
    public override void Configure(EntityTypeBuilder<Ad> builder)
    {
        base.Configure(builder);
        builder.Property(ad => ad.AdNo).HasMaxLength(30).IsRequired();
        builder.Property(ad => ad.Title).HasMaxLength(500).IsRequired();
        builder.Property(ad => ad.FormerPrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(ad => ad.CurrentPrice).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(ad => ad.DiscountAmount).HasMaxLength(100).IsRequired();
        builder.Property(ad => ad.StarCount).HasDefaultValue(0);
        builder.Property(ad => ad.AdOrder).ValueGeneratedOnAdd();
        //builder.HasIndex(ad => ad.AdOrder).IsUnique();
        builder.Property(ad => ad.Showcase).IsRequired();
        builder.Property(ad => ad.AdDetail).HasMaxLength(1000);

        builder.HasOne(ad => ad.Employee).WithMany(employee => employee.Ads)
            .HasForeignKey(ad => ad.EmployeeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(ad => ad.Product).WithMany(product => product.Ads)
            .HasForeignKey(ad => ad.ProductId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(ad => ad.Comments).WithOne(comment => comment.Ad)
            .HasForeignKey(comment => comment.AdId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Ad()
        {
            Id = 1,
            ProductId = 1,
            EmployeeId = 1,
            AdNo = "111111111",
            Title = "Ford Focus 1.6 Fren Balatası Orijinal",
            AdDate = DateTime.Now,
            FormerPrice = 550000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%25",
            StarCount =5,
            AdDetail = "FORD FOCUS KASA ARKA FREN DİSKİ RULMANLI 1 ADET FİYATIDIR. MAİS RENAULT ORJİNAL ÜRÜNÜDÜR.",
        }, new Ad()
        {
            Id = 2,
            ProductId = 2,
            EmployeeId = 2,
            AdNo = "11111112",
            Title = "Ford Corier 1.5 Debriyaj Seti Gıcırmı Gıcır",
            AdDate = DateTime.Now,
            FormerPrice = 450000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%0",
            StarCount = 3,
            AdDetail = "Ford Corier YENİ KASA DEBRİYAJ  SETİ 1 ADET FİYATIDIR. Orijinal BOSCH ÜRÜNÜDÜR.",
        }, new Ad()
        {
            Id = 3,
            ProductId = 3,
            EmployeeId = 1,
            AdNo = "11111113",
            Title = "Fiat Linea 1.3 Fren Diski",
            AdDate = DateTime.Now,
            FormerPrice = 650000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%20",
            StarCount = 4,
            AdDetail = "Ford Linea YENİ KASA Fren Diski 1 ADET FİYATIDIR. Orijinal BOSCH ÜRÜNÜDÜR.",
        }, new Ad()
        {
            Id = 4,
            ProductId = 4,
            EmployeeId = 2,
            AdNo = "11111114",
            Title = "Opel Astra 1.6 CDTI Fren Balatası",
            AdDate = DateTime.Now,
            FormerPrice = 470000.50m,
            CurrentPrice = 450000.50m,
            DiscountAmount = "%5",
            StarCount = 5,
            AdDetail = "Opel Astra 1.6 CDTI YENİ KASA Fren Balatası 1 ADET FİYATIDIR. Orijinal General Motor ÜRÜNÜDÜR.",
        });
    }
}
