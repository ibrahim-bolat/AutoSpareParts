using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public sealed class VehicleAddressMap:BaseEntityMap<VehicleAddress>
    {
        public override void Configure(EntityTypeBuilder<VehicleAddress> builder)
        {
            base.Configure(builder);
            builder.Property(vehicleAddress => vehicleAddress.AddressTitle).HasMaxLength(100).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.NeighborhoodOrVillage).HasMaxLength(250).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.District).HasMaxLength(250).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.City).HasMaxLength(250).IsRequired();
            builder.Property(vehicleAddress => vehicleAddress.PostalCode).HasMaxLength(5);
            builder.Property(vehicleAddress => vehicleAddress.AddressDetails).HasMaxLength(500).IsRequired();

        builder.HasData(new VehicleAddress
            {
                Id = 1,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            },new VehicleAddress
            {
                Id = 2,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            },new VehicleAddress
            {
                Id = 3,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            },new VehicleAddress
            {
                Id = 4,
                AddressTitle = "Evim",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye"
            });

        }
    }
