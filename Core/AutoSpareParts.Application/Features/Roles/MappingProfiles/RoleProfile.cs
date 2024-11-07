using AutoMapper;
using AutoSpareParts.Application.Features.Roles.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.Roles.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<Employee, RoleDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ReverseMap();
    }
}