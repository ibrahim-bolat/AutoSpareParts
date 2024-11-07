using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

//Siparişlerin Genel Tablosu
public class Order : BaseEntity
{
    public string OrderCode { get; set; } //ABCD6E-756 gibi
    public OrderStatus OrderStatus { get; set; }  //Gönderildi, Gönderilmedi, İptal Edildi Gibi
    public int TotalProductQuantity { get; set; }
    public Payment Payment { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}
