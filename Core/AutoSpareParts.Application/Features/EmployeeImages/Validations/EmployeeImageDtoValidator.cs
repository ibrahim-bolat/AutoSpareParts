using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.EmployeeImages.Validations;

public class EmployeeImageDtoValidator:AbstractValidator<EmployeeImageDto>
{
    public EmployeeImageDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(x => x.Path)
            .NotNull()
            .WithMessage("Resim Yolu boş olamaz....")
            .NotEmpty()
            .WithMessage("Resim Yolu boş olamaz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");

        RuleFor(x => x.AltText)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(x => x.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}