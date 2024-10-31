using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class Payment : BaseEntity
{

    public PaymentType PaymentType { get; set; } //Nakit vb
    public string Provider { get; set; }  // Mastercard vb dir galiba
    public string AccountNo { get; set; }  // Kart No
    public DateOnly Expiry { get; set; }   // Son Tarih
    public string SecurityCode { get; set; }   // CVV 155 gibi
    public decimal TotalPrice { get; set; }   //500 bin tl
    public Order Order { get; set; }

}
