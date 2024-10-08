using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpareParts.Application.Features.Addresses.DTOs;

public record DetailAddressDto:BaseDto
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
    public  string StreetName  { get; init; }

    [Display(Name = "Mahalle ya da Köy")]
    public  string NeighborhoodOrVillageName  { get; init; }

    [Display(Name = "İlçe")]
    public  string DistrictName  { get; init; }

    [Display(Name = "İl")]
    public  string CityName  { get; init; }
    
    [Display(Name = "Posta Kodu")]
    public  string PostalCode{ get; init; }
    
    [Display(Name = "Detaylı Adres")]
    public  string AddressDetails{ get; init; }
    
    [Display(Name = "Varsayılan Adresmi?")]
    public bool DefaultAddress { get; init; }
    
    public int UserId { get; init; }
}