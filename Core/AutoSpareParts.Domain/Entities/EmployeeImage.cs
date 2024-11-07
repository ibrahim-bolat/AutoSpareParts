using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;
public class EmployeeImage : BaseImage
{
    public bool Profil { get; set; }
    public int UserImageOrder { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
