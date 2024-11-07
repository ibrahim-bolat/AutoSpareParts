using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Validations;

public class ForgetEmployeePasswordDtoValidator : AbstractValidator<ForgetEmployeePasswordDto>
{
    public ForgetEmployeePasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

    }
}