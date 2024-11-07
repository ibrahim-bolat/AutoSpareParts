using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;
public class CustomerImage : BaseImage
{
    public bool Profil { get; set; }
    public int UserImageOrder { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
