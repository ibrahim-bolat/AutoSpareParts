using System.ComponentModel.DataAnnotations.Schema;
using AutoSpareParts.Domain.Entities.Common;


namespace AutoSpareParts.Domain.Entities.NotDerivedFromBase;

public class City : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<District> Districts{ get; set; }
}
