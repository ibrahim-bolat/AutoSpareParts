using System.Net;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.MainCategories.Validations;

public class MainCategoryDtoValidator : AbstractValidator<MainCategoryDto>
{
    public MainCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Lütden Kategori adını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Kategori adını boş geçmeyiniz....");
    }
}