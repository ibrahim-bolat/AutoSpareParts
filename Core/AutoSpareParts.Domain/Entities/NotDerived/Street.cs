using System.ComponentModel.DataAnnotations.Schema;
using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities.NotDerived;

public class Street : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int NeighborhoodOrVillageId { get; set; }
    public NeighborhoodOrVillage NeighborhoodOrVillage { get; set; }
}