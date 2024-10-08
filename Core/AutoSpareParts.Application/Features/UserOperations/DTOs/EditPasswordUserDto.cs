using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserOperations.DTOs;

public record EditPasswordUserDto:BaseDto
    {
        public int Id { get; init; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; init; }

        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; init; }

        [Display(Name = "Yeni Şifre Tekrar")]
        public string ReNewPassword { get; init; }
    }

