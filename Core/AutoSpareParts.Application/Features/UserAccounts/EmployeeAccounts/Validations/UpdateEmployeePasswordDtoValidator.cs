using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Validations;

public class UpdateEmployeePasswordDtoValidator : AbstractValidator<UpdateEmployeePasswordDto>
{
    public UpdateEmployeePasswordDtoValidator()
    {
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