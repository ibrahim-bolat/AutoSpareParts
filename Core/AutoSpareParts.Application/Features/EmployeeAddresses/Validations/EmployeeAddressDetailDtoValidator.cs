using System.Text.RegularExpressions;
using AutoSpareParts.Application.Features.EmployeeAddresses.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.EmployeeAddresses.Validations;

public class EmployeeAddressDetailDtoValidator:AbstractValidator<EmployeeAddressDetailDto>
{
    public EmployeeAddressDetailDtoValidator()
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
        
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .WithMessage("Lütfen telefonu boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen telefonu boş geçmeyiniz...")
            .MinimumLength(17).WithMessage("Telefon en az 17 karakter olabilir.")
            .MaximumLength(17).WithMessage("Telefon en fazla 17 karakter olabilir.")
            .Matches(new Regex(@"^((\+90))\(?([0-9]{3})\)?([0-9]{3})[-]?([0-9]{2})[-]?([0-9]{2})$"))
            .WithMessage("Lütfen uygun formatta telefon giriniz.");

        RuleFor(x => x.AddressTitle)
            .NotNull()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");

        RuleFor(x => x.AddressType)
            .NotNull()
            .WithMessage("Lütden adres tipini boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres tipini boş geçmeyiniz....");

        RuleFor(x => x.CityName)
            .NotNull()
            .WithMessage("Lütden ili boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ili boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(x => x.DistrictName)
            .NotNull()
            .WithMessage("Lütden ilçeyi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ilçeyi boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");

        RuleFor(x => x.NeighborhoodOrVillageName)
            .NotNull()
            .WithMessage("Lütden mahalle yada köyü boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden mahalle yada köyü boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
        RuleFor(x => x.StreetName)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
        RuleFor(x => x.PostalCode)
            .NotNull()
            .WithMessage("Lütfen posta kodunu boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen posta kodunu boş geçmeyiniz...")
            .Length(5)
            .WithMessage("Lütfen posta kodunu 5 karakter olarak giriniz...")
            .Matches(new Regex(@"^\d{5}$"))
            .WithMessage("Lütfen posta kodunu sadece sayısal değerler giriniz.");

        RuleFor(x => x.AddressDetails)
            .NotNull()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
        RuleFor(x => x.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}