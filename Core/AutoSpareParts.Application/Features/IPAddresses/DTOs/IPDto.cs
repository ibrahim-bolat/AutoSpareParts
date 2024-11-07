using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.IPAddresses.DTOs;

public record IPDto:BaseDto
    {
        public int Id { get; init; }
        
        [Display(Name = "IP Aralık Başlangıcı")]
        public string RangeStart { get; init; }
        
        [Display(Name = "IP Aralık Sonu")]
        public string RangeEnd { get; init; }
        
        [Display(Name = "IP Liste Tipi")]
        public  IPListType IPListType { get; init; }
        
        [Display(Name = "Durumu")]
        public bool Status { get; init; }

    }
