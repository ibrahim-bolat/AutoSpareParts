using AutoMapper;
using AutoSpareParts.Application.DTOs;
using AutoSpareParts.Application.Features.UserOperations.DTOs;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Application.Features.UserOperations.MappingProfiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, CreateUserDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryDto>().ReverseMap();
        CreateMap<AppUser, EditPasswordUserDto>().ReverseMap();
    }
}