using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserOperations.DTOs;


public record UserSummaryDto:BaseDto
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

    }