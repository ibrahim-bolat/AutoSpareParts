using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }    //Abs Sensörü gibi
    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } //Fren Gibi
    public List<Product> Products { get; set; }
}
