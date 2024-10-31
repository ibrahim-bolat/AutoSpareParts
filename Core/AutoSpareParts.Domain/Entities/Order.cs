using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class Order : BaseEntity
{
    public int TotalProductQuantity { get; set; }
    public Payment Payment { get; set; }
    public int UserId { get; set; }
    public AppUser AppUser { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }
}
