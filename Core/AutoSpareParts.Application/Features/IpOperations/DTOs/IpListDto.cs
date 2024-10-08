using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.IpOperations.DTOs;

public record IpListDto:BaseDto
    {
        public int Id { get; init; }
        
        [Display(Name = "Ip Aralık Başlangıcı")]
        public string RangeStart { get; init; }
        
        [Display(Name = "Ip Aralık Sonu")]
        public string RangeEnd { get; init; }
        
        [Display(Name = "Ip Liste Tipi")]
        public  string IpListType { get; init; }
        
        [Display(Name = "Durumu")]
        public bool Status { get; init; }

    }
