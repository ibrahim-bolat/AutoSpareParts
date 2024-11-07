using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;

public class Employee : AppUser
{
    public string Title { get; set; } // Manager, Sales Representative, Director gibi
    public DateOnly HireDate { get; set; } // Manager, Sales Representative, Director gibi
    public List<Ad> Ads { get; set; }
    public List<Order> Orders { get; set; }
    public List<EmployeeAddress> EmployeeAddresses { get; set; }
    public List<EmployeeImage> EmployeeImages { get; set; }
}
