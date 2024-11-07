using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class SupplierAddressMap : BaseEntityMap<SupplierAddress>
{
    public override void Configure(EntityTypeBuilder<SupplierAddress> builder)
    {
        base.Configure(builder);

        builder.HasOne(supplierAddress => supplierAddress.Supplier).WithMany(supplier => supplier.SupplierAddresses)
            .HasForeignKey(supplierAddress => supplierAddress.SupplierId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new SupplierAddress()
        {
            Id = 1,
            SupplierId = 1,
        }, new SupplierAddress()
        {
            Id = 2,
            SupplierId = 2,
        });
    }
}
