using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class Discount : BaseEntity
{
    public string Name { get; set; }  // Büyük Bahar İndirimi
    public string DiscountDetail { get; set; }  // uzun açıklamlar
    public decimal Percent { get; set; }  // Yüzde 5 vb
    public List<Product> Products { get; set; }

}
