using AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.UserAccounts.EmployeeAccounts.Validations;

public class EmployeeAddressSummaryDtoValidator : AbstractValidator<EmployeeAddressSummaryDto>
{
    public EmployeeAddressSummaryDtoValidator()
    {
        RuleFor(x => x.AddressTitle)
            .NotNull()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");

        RuleFor(x => x.AddressDetails)
            .NotNull()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}