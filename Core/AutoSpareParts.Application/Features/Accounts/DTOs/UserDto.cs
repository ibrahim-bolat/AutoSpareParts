using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Application.Features.Accounts.Validations.CustomValidations;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.Accounts.DTOs;

public record UserDto:BaseDto
    {
        public int Id { get; init; }
        
        [Display(Name = "Ad")]
        public string FirstName { get; init; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; init; }
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; init; }
        
        [Display(Name = "Cinsiyet")]
        public GenderType GenderType { get; init; }
        
        [Display(Name = "Kimlik No")]
        public string UserIdendityNo { get; init; }
        
        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; init; }
        
        [Display(Name = "Email")]
        public string Email { get; init; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}",ApplyFormatInEditMode = false)]
        [Display(Name = "Doğum Tarihi")]
        [CustomDate]
        public DateOnly? DateOfBirth{get; init;}
        
        [Display(Name = "Not")]
        public string Note { get; init; }
        
    }
