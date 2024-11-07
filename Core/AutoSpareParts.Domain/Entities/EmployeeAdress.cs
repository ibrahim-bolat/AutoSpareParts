using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class EmployeeAddress : Address
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressType AddressType { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

}
