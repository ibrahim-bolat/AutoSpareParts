using AutoMapper;
using AutoSpareParts.Application.Features.UserImages.DTOs;
using AutoSpareParts.Domain.Entities;

namespace AutoSpareParts.Application.Features.UserImages.MappingProfiles;

public class UserImageProfile:Profile
{
    public UserImageProfile()
    {
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<UserImage, CreateUserImageDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
        CreateMap<UserImageDto, CreateUserImageDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
    }
}