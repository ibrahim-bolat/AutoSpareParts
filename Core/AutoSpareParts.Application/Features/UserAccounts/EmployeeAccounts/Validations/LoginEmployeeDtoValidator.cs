using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Validations;

public class LoginEmployeeDtoValidator : AbstractValidator<LoginEmployeeDto>
{
    public LoginEmployeeDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Lütfen şifenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...");
    }
}