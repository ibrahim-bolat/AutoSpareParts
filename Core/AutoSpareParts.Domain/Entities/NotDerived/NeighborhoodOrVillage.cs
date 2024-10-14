using System.ComponentModel.DataAnnotations.Schema;
using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities.NotDerived;

public class NeighborhoodOrVillage : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DistrictId { get; set; }
    public District District { get; set; }
    public List<Street> Streets { get; set; }
}