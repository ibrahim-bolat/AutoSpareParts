using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;

public record ForgetEmployeePasswordDto : BaseDto
{
    [Display(Name = "E-Posta Adresiniz")]
    public string Email { get; init; }
}
