using AutoMapper;
using AutoSpareParts.Application.Features.Accounts.DTOs;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Application.Features.Accounts.MappingProfiles;

public class AccountProfile:Profile
{
    public AccountProfile()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryCardDto>().ForMember(dest => dest.DefaultAddressDetail
                , opt => opt.MapFrom(src => src.Addresses.FirstOrDefault(x=>x.DefaultAddress).AddressDetails))
            .ReverseMap();
        CreateMap<AppUser, EditPasswordAccountDto>().ReverseMap(); 
        CreateMap<Address, AddressSummaryDto>() 
            .ForMember(dest => dest.FullName
                , opt => opt.MapFrom(src => src.FirstName+" "+src.LastName))
            .ReverseMap();
    }
}