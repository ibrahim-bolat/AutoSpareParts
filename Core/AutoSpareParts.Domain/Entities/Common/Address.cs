
namespace AutoSpareParts.Domain.Entities.Common;

public class Address : BaseEntity
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AddressTitle { get; set; }
    public string CityId { get; set; }
    public string CityName { get; set; }
    public string DistrictId { get; set; }
    public string DistrictName { get; set; }
    public string NeighborhoodOrVillageId { get; set; }
    public string NeighborhoodOrVillageName { get; set; }
    public string StreetId { get; set; }
    public string StreetName { get; set; }
    public string PostalCode { get; set; }
    public string AddressDetails { get; set; }
    public bool DefaultAddress { get; set; }
}
