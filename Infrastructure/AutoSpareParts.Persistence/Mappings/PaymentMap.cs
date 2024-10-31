using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Enums;
using AutoSpareParts.Persistence.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoSpareParts.Persistence.Mappings;

public sealed class PaymentMap : BaseEntityMap<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);
        builder.Property(payment => payment.PaymentType).HasConversion(a => a.ToString(), a => (PaymentType)Enum.Parse(typeof(PaymentType), a)).IsRequired();
        builder.Property(payment => payment.Provider).HasMaxLength(250);
        builder.Property(payment => payment.AccountNo).HasMaxLength(250);
        builder.Property(payment => payment.Expiry).HasMaxLength(200);
        builder.Property(payment => payment.SecurityCode).HasMaxLength(100);
        builder.Property(payment => payment.TotalPrice).HasColumnType("decimal(18,4)").IsRequired();

        builder.HasOne(payment => payment.Order).WithOne(order => order.Payment)
            .HasForeignKey<Payment>(payment => payment.Id).OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Payment()
        {
            Id = 1,
            PaymentType = PaymentType.CreditCard,
            Provider = "MasterCard",
            AccountNo = "2556 7584 9664 4868",
            Expiry = DateOnly.FromDateTime(DateTime.Now),
            SecurityCode = "155"
        }, new Payment()
        {
            Id = 2,
            PaymentType = PaymentType.Transfer,
            Provider = "Visa",
            AccountNo = "2556 7584 9666 4869",
            Expiry = DateOnly.FromDateTime(DateTime.Now),
            SecurityCode = "154"
        }, new Payment()
        {
            Id = 3,
            PaymentType = PaymentType.Cash,
            Expiry = DateOnly.FromDateTime(DateTime.Now),
            SecurityCode = "153"
        }, new Payment()
        {
            Id = 4,
            PaymentType = PaymentType.CreditCard,
            Provider = "MasterCard",
            AccountNo = "2552 7583 9664 4860",
            Expiry = DateOnly.FromDateTime(DateTime.Now),
            SecurityCode = "152",
        });
    }
}
