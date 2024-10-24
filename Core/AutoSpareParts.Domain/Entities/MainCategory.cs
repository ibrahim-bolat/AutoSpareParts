using AutoSpareParts.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Domain.Entities;

public class MainCategory : BaseEntity
{
    public string Name { get; set; }  //Fren Gibi
    public int MainCategoryOrder { get; set; } //  5.sırada gibi
    public List<Category> Categories { get; set; }
}
