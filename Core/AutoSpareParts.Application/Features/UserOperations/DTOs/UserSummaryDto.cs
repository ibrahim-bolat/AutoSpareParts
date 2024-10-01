using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserOperations.DTOs;


public class UserSummaryDto:BaseDto
    {
        public string Id { get; set; }

        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        public string Email { get; set; }

    }