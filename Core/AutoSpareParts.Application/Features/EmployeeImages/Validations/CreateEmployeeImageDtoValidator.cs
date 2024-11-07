using AutoSpareParts.Application.Features.EmployeeImages.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.EmployeeImages.Validations;

public class CreateEmployeeImageDtoValidator:AbstractValidator<CreateEmployeeImageDto>
{
    public CreateEmployeeImageDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");

        RuleFor(x => x.AltText)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(x => x.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");

        RuleFor(x => x.ImageFile)
            .NotNull()
            .WithMessage("Lütden resimi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resimi boş geçmeyiniz....");
    }
}