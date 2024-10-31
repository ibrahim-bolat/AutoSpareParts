using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class OrderDetail : BaseEntity
{
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public DateTime ModifiedTime { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public string CreatedByName { get; set; } = RoleType.Admin.ToString();
    public string ModifiedByName { get; set; } = RoleType.Admin.ToString();
    public string Note { get; set; }
    public decimal UnitPrice { get; set; }  
    public int Quantity { get; set; }  
    public decimal Percent { get; set; }  // Yüzde 5 vb
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
