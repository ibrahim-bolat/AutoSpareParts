using System.Text.RegularExpressions;
using AutoSpareParts.Application.Features.Accounts.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.Accounts.Validations;

public class UserDtoValidator:AbstractValidator<UserDto>
{
    public UserDtoValidator()
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
        
        RuleFor(x => x.PhoneNumber)
            .Matches(new Regex(@"^((\+90))\(?([0-9]{3})\)?([0-9]{3})[-]?([0-9]{2})[-]?([0-9]{2})$"))
            .WithMessage("Lütfen uygun formatta telefon giriniz.");
    }
}