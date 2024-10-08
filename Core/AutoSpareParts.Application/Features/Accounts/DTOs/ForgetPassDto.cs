using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;

public record ForgetPassDto:BaseDto
    {
        [Display(Name = "E-Posta Adresiniz")]
        public string Email { get; init; }
    }
