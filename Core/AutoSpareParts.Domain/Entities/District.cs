using System.ComponentModel.DataAnnotations.Schema;
using AutoSpareParts.Domain.Entities.Common;


namespace AutoSpareParts.Domain.Entities;

public class District : IEntity
{
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<NeighborhoodOrVillage> NeighborhoodsOrVillages{ get; set; }
}
