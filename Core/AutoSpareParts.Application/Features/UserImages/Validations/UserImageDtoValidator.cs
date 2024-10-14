using AutoSpareParts.Application.Features.UserImages.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.UserImages.Validations;

public class UserImageDtoValidator:AbstractValidator<UserImageDto>
{
    public UserImageDtoValidator()
    {
        RuleFor(userImage => userImage.Title)
            .NotNull()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(userImage => userImage.Path)
            .NotNull()
            .WithMessage("Resim Yolu boş olamaz....")
            .NotEmpty()
            .WithMessage("Resim Yolu boş olamaz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");

        RuleFor(userImage => userImage.AltText)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(userImage => userImage.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}