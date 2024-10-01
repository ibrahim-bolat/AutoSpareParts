using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;

public class BrandSeries : BaseEntity
{
    public string Name { get; set; }    //Golf gibi
    public int BrandId { get; set; }
    public Brand Brand { get; set; } 
    public List<Model> Models { get; set; }
}
