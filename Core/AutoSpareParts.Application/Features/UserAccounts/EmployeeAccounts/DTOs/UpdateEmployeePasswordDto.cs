using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;


public record UpdateEmployeePasswordDto : BaseDto
{
    [Display(Name = "Yeni Şifre")]
    public string Password { get; init; }

    [Display(Name = "Yeni Şifre Tekrar")]
    [Compare("Password", ErrorMessage = "Şifre ile eşleşmelidir")]
    public string RePassword { get; init; }
}
