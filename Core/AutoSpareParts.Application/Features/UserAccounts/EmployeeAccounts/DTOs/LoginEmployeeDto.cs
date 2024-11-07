using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;


public record LoginEmployeeDto : BaseDto
{
    [Display(Name = "E-Posta ")]
    public string Email { get; init; }


    [Display(Name = "Şifre")]
    public string Password { get; init; }


    [Display(Name = "Beni Hatırla")]
    public bool Persistent { get; init; }

    public bool Lock { get; init; }
}
