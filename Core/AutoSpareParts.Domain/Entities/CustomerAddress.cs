using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class CustomerAddress : Address
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressType AddressType { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
