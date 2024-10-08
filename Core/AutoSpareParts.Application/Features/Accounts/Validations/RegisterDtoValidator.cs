using AutoSpareParts.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.Accounts.Validations;

public class RegisterDtoValidator:AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(x => x.LastName)
            .NotNull()
            .WithMessage("Lütfen soyadınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen soyadınızı boş geçmeyiniz...")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .MaximumLength(30)
            .WithMessage("En fazla 30 karakter girebilirsiniz...")
            .NotEqual(x => x.FirstName)
            .WithMessage("Kullanıcı Adı Adla aynı olamaz...")
            .NotEqual(x => x.LastName)
            .WithMessage("Kullanıcı Adı Soyadla aynı olamaz...");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...");


        RuleFor(x => x.RePassword)
            .NotNull()
            .WithMessage("Lütfen şifre tekrarı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifre tekrarı boş geçmeyiniz...")
            .Equal(x => x.Password)
            .WithMessage("Lütfen şifre ile aynı giriniz...");
    }
}