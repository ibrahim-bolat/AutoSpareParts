using AutoMapper;
using AutoSpareParts.Application.Features.RoleOperations.DTOs;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Application.Features.RoleOperations.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, RoleDto>().ForMember(dest=>dest.Status,
                opt=>opt.MapFrom(src => src.IsActive))
            .ReverseMap();
    }
}