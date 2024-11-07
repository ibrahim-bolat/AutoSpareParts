using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;

public record EditEmployeePasswordDto : BaseDto
{
    public int Id { get; init; }

    [Display(Name = "Kullanıcı Adı")]
    public string UserName { get; init; }

    [Display(Name = "Yeni Şifre")]
    public string NewPassword { get; init; }

    [Display(Name = "Yeni Şifre Tekrar")]
    [Compare("NewPassword", ErrorMessage = "Şifre ile eşleşmelidir")]
    public string ReNewPassword { get; init; }
}

