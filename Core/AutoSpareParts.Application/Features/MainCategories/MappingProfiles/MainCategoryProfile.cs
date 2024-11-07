using AutoMapper;
using AutoSpareParts.Application.Extensions;
using AutoSpareParts.Application.Features.IPAddresses.DTOs;
using AutoSpareParts.Application.Features.MainCategories.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.MainCategories.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<MainCategory, MainCategoryDto>().ReverseMap();
        CreateMap<MainCategory, MainCategoryListDto>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive)).ReverseMap();
    }
}