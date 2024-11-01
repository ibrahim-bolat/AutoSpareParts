using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

// Order tablosundaki her bir siparişin detay tablosu
public class OrderDetail : BaseEntity
{
    public decimal UnitPrice { get; set; }  
    public decimal DiscountPercent { get; set; }  // Yüzde 5 vb
    public int Quantity { get; set; }  
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
