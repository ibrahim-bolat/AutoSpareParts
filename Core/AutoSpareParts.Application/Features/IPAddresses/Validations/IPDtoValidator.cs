using System.Net;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using FluentValidation;

namespace AutoSpareParts.Application.Features.IPAddresses.Validations;

public class IPDtoValidator:AbstractValidator<IPDto>
{
    public IPDtoValidator()
    {
        RuleFor(x => x.RangeStart)
            .NotNull()
            .WithMessage("Lütden Ip Aralık Başlangıcını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Ip Aralık Başlangıcını boş geçmeyiniz....")
            .Must(IsValidIp)
            .WithMessage("Lütfen Ip Adresini Doğru Giriniz....");
        RuleFor(x => x.RangeEnd)
            .NotNull()
            .WithMessage("Lütden Ip Aralık Sonunu boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Ip Aralık Sonunu boş geçmeyiniz....")
            .Must(IsValidIp)
            .WithMessage("Lütfen Ip Adresini Doğru Giriniz....");
        RuleFor(x => x.IPListType)
            .NotNull()
            .WithMessage("Lütden Ip Listesi tipini boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden Ip Listesi tipini boş geçmeyiniz....");
    }
    private bool IsValidIp(string IP)
    {
        IPAddress IPAddress;
        return IPAddress.TryParse(IP, out IPAddress) && IPAddress.ToString() == IP;
    }
}