using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserOperations.DTOs;


public record CreateUserDto:BaseDto
    {
        [Display(Name = "Ad")]
        public string FirstName { get; init; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; init; }
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; init; }
        
        [Display(Name = "Email")]
        public string Email { get; init; }
        
        [Display(Name = "Şifre")]
        public string Password { get; init; }

        [Display(Name = "Şifre Tekrar")]
        public string RePassword { get; init; }
    }

