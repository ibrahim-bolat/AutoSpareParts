using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;


public record UpdatePasswordDto:BaseDto
    {
        [Display(Name = "Yeni Şifre")]
        public string Password { get; init; }
        
        [Display(Name = "Yeni Şifre Tekrar")]
        public string RePassword { get; init; }
    }
