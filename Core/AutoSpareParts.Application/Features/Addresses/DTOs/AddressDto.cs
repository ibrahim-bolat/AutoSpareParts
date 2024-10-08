using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.DTOs;

public record AddressDto:BaseDto
{
    public int Id { get; init; }
    
    [Display(Name = "Not")]
    public  string Note { get; init; }
    
    [Display(Name = "Adı")]
    public string FirstName { get; init; }
    
    [Display(Name = "Soyadı")]
    public string LastName { get; init; }
    
    [Display(Name = "Email")]
    public string Email{ get; init; }
    
    [Display(Name = "Telefon")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber{ get; init; }
    
    [Display(Name = "Adres Başlığı")]
    public  string AddressTitle{ get; init; }
    
    [Display(Name = "Adres Tipi")]
    public  AddressType AddressType { get; init; }
    
    [Display(Name = "Cadde ya da Sokak")]
    public  string StreetId  { get; init; }
    public  List<SelectListItem> Streets { get; set; }

    [Display(Name = "Mahalle ya da Köy")]
    public  string NeighborhoodOrVillageId  { get; init; }
    public  List<SelectListItem> NeighborhoodsOrVillages { get; set; }
    
    [Display(Name = "İlçe")]
    public  string DistrictId  { get; init; }
    public  List<SelectListItem> Districts { get; set; }
    
    [Display(Name = "İl")]
    public  string CityId  { get; init; }
        
    public  List<SelectListItem> Cities { get; set; }

    [Display(Name = "Posta Kodu")]
    public  string PostalCode{ get; init; }
    
    [Display(Name = "Detaylı Adres")]
    public  string AddressDetails{ get; init; }
    
    [Display(Name = "Varsayılan Adresmi?")]
    public bool DefaultAddress { get; init; }
    
    public int UserId { get; init; }
}