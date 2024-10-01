using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;

    public class Brand : BaseEntity
    {
        public string Name { get; set; }  //Volkswagen gibi
        public List<BrandSeries> BrandSeries { get; set; }
    }
