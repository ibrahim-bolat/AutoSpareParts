using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;


public record UserSummaryCardDto:BaseDto
    {
        public string Id { get; init; }

        [Display(Name = "Ad")]
        public string FirstName { get; init; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; init; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; init; }
        
        [Display(Name = "Email")]
        public string Email { get; init; }

        [Display(Name = "Cinsiyet")]
        public GenderType GenderType { get; init; }
        
        [Display(Name = "Varsayılan Adres")]
        public string DefaultAddressDetail { get; init; }
        
    }