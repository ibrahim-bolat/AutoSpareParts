using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.IPAddresses.DTOs;


public record AssignIPAddressDto : BaseDto
{
    public int Id { get; init; }

    [Display(Name = "IP Aralık Başlangıcı")]
    public string RangeStart { get; init; }

    [Display(Name = "IP Aralık Sonu")]
    public string RangeEnd { get; init; }

    [Display(Name = "IP Liste Tipi")]
    public string IPListType { get; init; }

    [Display(Name = "Area Adı")]
    public string TobeAssignedAreaName { get; init; }

    [Display(Name = "Menu Adı")]
    public string TobeAssignedMenuName { get; init; }

    public string TobeAssignedEndpointId { get; init; }
    public bool HasAssign { get; init; }
}
