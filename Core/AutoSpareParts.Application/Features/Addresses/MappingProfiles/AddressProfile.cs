using AutoMapper;
using AutoSpareParts.Application.Features.Addresses.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.Addresses.MappingProfiles;

public class AddressProfile:Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, DetailAddressDto>().ReverseMap();
    }
}