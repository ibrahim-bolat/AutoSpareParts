using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserOperations.DTOs;

public class EditPasswordUserDto:BaseDto
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }

        [Display(Name = "Yeni Şifre Tekrar")]
        public string ReNewPassword { get; set; }
    }

