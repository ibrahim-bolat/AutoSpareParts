using AutoMapper;
using AutoSpareParts.Application.Extensions;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.IPAddresses.MappingProfiles;

public class IPProfile:Profile
{
    public IPProfile()
    {
        CreateMap<IPAddress, IPDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ReverseMap();
        CreateMap<IPAddress, IPListDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ForMember(dest=>dest.IPListType,opt=>opt.MapFrom(src=>src.IPListType.GetEnumDescription()))
            .ReverseMap();
    }
}