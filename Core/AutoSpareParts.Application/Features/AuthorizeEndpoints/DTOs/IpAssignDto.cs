using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.AuthorizeEndpoints.DTOs;


public record IpAssignDto:BaseDto
    {
        public int Id { get; init; }
        
        [Display(Name = "Ip Aralık Başlangıcı")]
        public string RangeStart { get; init; }
        
        [Display(Name = "Ip Aralık Sonu")]
        public string RangeEnd { get; init; }
        
        [Display(Name = "Ip Liste Tipi")]
        public  string IpListType { get; init; }

        [Display(Name = "Area Adı")]
        public  string TobeAssignedAreaName { get; init; }
        
        [Display(Name = "Menu Adı")]
        public  string TobeAssignedMenuName { get; init; }
        
        public  string TobeAssignedEndpointId { get; init; }
        public bool HasAssign { get; init; }
    }
