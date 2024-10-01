using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;

public class ForgetPassDto:BaseDto
    {
        [Display(Name = "E-Posta Adresiniz")]
        public string Email { get; set; }
    }
