using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.Features.MainCategories.DTOs;

public record MainCategoryDto : BaseDto
{
    public int Id { get; init; }

    [Display(Name = "Ana Kategori Adı")]
    public string Name { get; init; }

}
