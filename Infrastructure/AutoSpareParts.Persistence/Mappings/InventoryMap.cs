using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class InventoryMap : BaseEntityMap<Inventory>
{
    public override void Configure(EntityTypeBuilder<Inventory> builder)
    {
        base.Configure(builder);
        builder.Property(inventory => inventory.StockCode).HasMaxLength(250).IsRequired();
        builder.Property(inventory => inventory.StockStatus).IsRequired();
        builder.Property(inventory => inventory.StockQuantity).HasDefaultValue(0).IsRequired();

        builder.HasOne(inventory => inventory.Product).WithOne(product => product.Inventory)
         .HasForeignKey<Inventory>(inventory => inventory.Id).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Inventory()
        {
            Id = 1,
            StockCode = "IVECO5802794866",
            StockStatus = true,
            StockQuantity = 500,
        }, new Inventory()
        {
            Id = 2,
            StockCode = "IVECO5802794845",
            StockStatus = true,
            StockQuantity = 1000,
        }, new Inventory()
        {
            Id = 3,
            StockCode = "IVECO5802794850",
            StockStatus = true,
            StockQuantity = 1500,
        }, new Inventory()
        {
            Id = 4,
            StockCode = "IVECO5802794870",
            StockStatus = true,
            StockQuantity = 2000,
        });
    }
}
