using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;

public class Customer : AppUser
{
    public List<Comment> Comments { get; set; }
    public List<Order> Orders { get; set; }
    public List<CustomerAddress> CustomerAddresses { get; set; }
    public List<CustomerImage> CustomerImages { get; set; }
}
