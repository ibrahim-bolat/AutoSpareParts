using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class SupplierMap : BaseEntityMap<Supplier>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
        base.Configure(builder);
        builder.Property(supplier => supplier.Name).HasMaxLength(250).IsRequired();

        builder.HasMany(supplier => supplier.Products).WithOne(product => product.Supplier)
            .HasForeignKey(product => product.SupplierId).OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(supplier => supplier.SupplierAddresses).WithOne(supplierAddress => supplierAddress.Supplier)
            .HasForeignKey(supplierAddress => supplierAddress.SupplierId).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Supplier()
        {
            Id = 1,
            Name = "BOLAT A.Þ"
        }, new Supplier()
        {
            Id = 2,
            Name = "ÞÝMÞEKLER LTD.ÞTÝ"
        });
    }
}
