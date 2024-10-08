using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;
public record AddressSummaryDto:BaseDto
{
    public int Id { get; init; }
    
    [Display(Name = "Ad Soyad")]
    public  string FullName{ get; init; }
    
    [Display(Name = "Telefon")]
    public string PhoneNumber { get; init; }
    
    [Display(Name = "Adres Başlığı")]
    public  string AddressTitle{ get; init; }
    
    [Display(Name = "Detaylı Adres")]
    public  string AddressDetails{ get; init; }
    
    [Display(Name = "Varsayılan Adres")]
    public bool DefaultAddress { get; init; }
}