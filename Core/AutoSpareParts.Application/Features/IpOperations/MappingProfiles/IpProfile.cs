using AutoMapper;
using AutoSpareParts.Application.Extensions;
using AutoSpareParts.Application.Features.IpOperations.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.IpOperations.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<IpAddress, IpDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ReverseMap();
        CreateMap<IpAddress, IpListDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ForMember(dest=>dest.IpListType,opt=>opt.MapFrom(src=>src.IpListType.GetEnumDescription()))
            .ReverseMap();
    }
}