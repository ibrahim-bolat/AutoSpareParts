using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.MainCategories.DTOs;

public record MainCategoryListDto : BaseDto
{
    public int Id { get; init; }

    [Display(Name = "Kategori Adı")]
    public string Name { get; init; }

    [Display(Name = "Ana Kategori Sırası")]
    public int MainCategoryOrder { get; init; }

    [Display(Name = "Güncellenme Zamanı")]
    public DateTime ModifiedTime { get; init; }

    [Display(Name = "Durumu")]
    public bool Status { get; init; }
}
