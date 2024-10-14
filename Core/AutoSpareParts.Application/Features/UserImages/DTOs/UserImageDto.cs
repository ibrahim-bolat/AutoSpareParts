
using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserImages.DTOs;

public record UserImageDto:BaseDto
{
    public int Id { get; set; }
    
    [Display(Name = "Not")]
    public string Note { get; set; }
    
    [Display(Name = "Resim Başlığı")]
    public string Title { get; set; }
    
    [Display(Name = "Resim Yolu")]
    public string Path { get; set; }
    
    [Display(Name = "Resim Kısa Açıklmaa")]
    public string AltText { get; set; }
    
    [Display(Name = "Profil Resmimi?")]
    public bool Profil { get; set; }
    
    public int UserId { get; set; }
}