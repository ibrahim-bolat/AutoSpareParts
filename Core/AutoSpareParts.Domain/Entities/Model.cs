using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

    public class Model : BaseEntity
    {
        public string Name { get; set; }   // Golf VIII (KD) (2019-?) gibi
        public string EngineType { get; set; } //1.4 TSI and 1.6 TDI gibi
        public EngineCapacityType EngineCapacity { get; set; } //1598 cc gibi
        public EnginePowerType EnginePower { get; set; } //115 Beygir gibi
        public string EquipmentVariant { get; set; } //Confortline gibi
        public int ModelYear { get; set; } //2005 gibi
        public FuelType FuelType { get; set; } //Dizel gibi
        public GearType GearType { get; set; } //Otomatik gibi
        public BodyType BodyType { get; set; } //Hatchback 5 kapı gibi

        public int BrandSeriesId { get; set; }
        public BrandSeries BrandSeries { get; set; }

        public List<Product> Products { get; set; }
    }

