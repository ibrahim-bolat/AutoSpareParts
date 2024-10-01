using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

    public class Address : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber{ get; set; }
        public  string AddressTitle{ get; set; }
        public  AddressType AddressType { get; set; }
        public  string CityId{ get; set; }
        public  string CityName{ get; set; }
        public  string DistrictId{ get; set; }
        public  string DistrictName{ get; set; }
        public  string NeighborhoodOrVillageId{ get; set; }
        public  string NeighborhoodOrVillageName{ get; set; }
        public  string StreetId{ get; set; }
        public  string StreetName{ get; set; }
        public  string PostalCode{ get; set; }
        public  string AddressDetails{ get; set; }
        public bool DefaultAddress { get; set; }
        public int UserId{ get; set; }
        public AppUser AppUser{ get; set; }
    
}
